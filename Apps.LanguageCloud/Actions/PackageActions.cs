using System.Net.Http.Headers;
using Apps.LanguageCloud.Constants;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.ProjectPackages.Requests;
using Apps.LanguageCloud.Models.ProjectPackages.Responses;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList("Packages")]
public class PackageActions(
    InvocationContext invocationContext,
    IFileManagementClient fileManagementClient) : LanguageCloudInvocable(invocationContext)
{
    [Action("Start project package export", Description = "Starts exporting selected project tasks to translation packages. If no task IDs are provided, active human target-file tasks are selected automatically.")]
    public async Task<ExportPackageResponse> ExportPackage([ActionParameter] ExportPackageRequest input)
    {
        var taskIds = input.TaskIds?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.Ordinal)
            .ToList() ?? [];

        if (taskIds.Count == 0)
        {
            taskIds = await GetEligibleProjectTaskIds(input.ProjectId);
        }

        if (taskIds.Count == 0)
        {
            throw new PluginApplicationException(
                "The project does not contain any active human target-file tasks that can be exported. " +
                "Wait for preprocessing to finish or provide eligible task IDs explicitly.");
        }

        var body = new Dictionary<string, object>
        {
            ["projectId"] = input.ProjectId,
            ["taskIds"] = taskIds
        };

        AddOptional(body, "onlinePackage", input.OnlinePackage);
        AddOptional(body, "includeTM", input.IncludeTm);
        AddOptional(body, "includeReferenceFiles", input.IncludeReferenceFiles);
        AddOptional(body, "splitByLanguage", input.SplitByLanguage);
        AddOptional(body, "filesPerPackage", input.FilesPerPackage);

        var request = new LanguageCloudRequest("/packages/export", Method.Post);
        request.AddJsonBody(body);

        return await Client.ExecuteWithErrorHandling<ExportPackageResponse>(request);
    }

    [Action("Download exported package", Description = "Downloads a completed translation package as an .sdlppx file.")]
    public async Task<DownloadPackageResponse> DownloadExportedPackage(
        [ActionParameter] PackageExportIdentifier input)
    {
        var request = new LanguageCloudRequest($"/packages/export/download/{input.ExportId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling(request);
        var fileBytes = response.RawBytes;

        if (fileBytes is null || fileBytes.Length == 0)
        {
            throw new PluginApplicationException("Trados returned an empty exported package.");
        }

        var fileName = GetResponseFileName(response) ?? $"Package_{input.ExportId}.sdlppx";
        var contentType = string.IsNullOrWhiteSpace(response.ContentType)
            ? "application/octet-stream"
            : response.ContentType;

        await using var stream = new MemoryStream(fileBytes);
        var file = await fileManagementClient.UploadAsync(stream, contentType, fileName);

        return new DownloadPackageResponse { File = file };
    }

    [Action("Import project package", Description = "Imports a completed .sdlppx or .sdlrpx translation package.")]
    public async Task<ImportPackageResponse> ImportPackage([ActionParameter] ImportPackageRequest input)
    {
        var extension = System.IO.Path.GetExtension(input.File.Name);
        if (!extension.Equals(".sdlppx", StringComparison.OrdinalIgnoreCase) &&
            !extension.Equals(".sdlrpx", StringComparison.OrdinalIgnoreCase))
        {
            throw new PluginMisconfigurationException("The package must be an .sdlppx or .sdlrpx file.");
        }

        await using var sourceStream = await fileManagementClient.DownloadAsync(input.File);
        using var buffer = new MemoryStream();
        await sourceStream.CopyToAsync(buffer);

        var request = new LanguageCloudRequest("/packages/import", Method.Post);
        request.AddJsonBody(new
        {
            file = Convert.ToBase64String(buffer.ToArray())
        });

        return await Client.ExecuteWithErrorHandling<ImportPackageResponse>(request);
    }

    private async Task<List<string>> GetEligibleProjectTaskIds(string projectId)
    {
        var tasks = await PaginationHelper.GetAllAsync<TaskDto>(async (top, skip) =>
        {
            var request = new LanguageCloudRequest($"/projects/{projectId}/tasks", Method.Get);
            request.AddQueryParameter("top", top);
            request.AddQueryParameter("skip", skip);
            request.AddQueryParameter("fields", FieldsConstants.PackageExportTask);

            return await Client.ExecuteWithErrorHandling<ResponseWrapper<List<TaskDto>>>(request);
        });

        return tasks
            .Where(IsEligiblePackageExportTask)
            .Select(x => x.Id)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.Ordinal)
            .ToList();
    }

    private static bool IsEligiblePackageExportTask(TaskDto task)
    {
        var isActive = string.Equals(task.Status, "created", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(task.Status, "inProgress", StringComparison.OrdinalIgnoreCase);
        var isHumanFileTask = task.TaskType is { Automatic: false } &&
                              string.Equals(task.TaskType.Scope, "file", StringComparison.OrdinalIgnoreCase);
        var hasTargetFileInput = task.input is not null &&
                                 (string.Equals(task.input.type, "targetFile", StringComparison.OrdinalIgnoreCase) ||
                                  task.input.targetFile is not null);

        return isActive && isHumanFileTask && hasTargetFileInput;
    }

    private static void AddOptional<T>(IDictionary<string, object> body, string name, T? value)
        where T : struct
    {
        if (value.HasValue)
        {
            body[name] = value.Value;
        }
    }

    private static string? GetResponseFileName(RestResponse response)
    {
        var header = response.ContentHeaders?
            .FirstOrDefault(x => string.Equals(x.Name, "Content-Disposition", StringComparison.OrdinalIgnoreCase))
            ?.Value?.ToString();

        if (string.IsNullOrWhiteSpace(header) ||
            !ContentDispositionHeaderValue.TryParse(header, out var contentDisposition))
        {
            return null;
        }

        return (contentDisposition.FileNameStar ?? contentDisposition.FileName)?.Trim('"');
    }
}
