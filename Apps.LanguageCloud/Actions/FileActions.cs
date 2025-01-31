using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using Apps.LanguageCloud.Models.Files.Responses;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Files.Requests;
using Blackbird.Applications.Sdk.Common.Actions;
using Newtonsoft.Json;
using System.Net.Mime;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using System.Linq;
using System.Text.RegularExpressions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using System.Web;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class FileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : LanguageCloudInvocable(invocationContext)
{
    [Action("List project source files", Description = "List project source files")]
    public async Task<ListAllFilesResponse> ListSourceFiles([ActionParameter] ListSourceFilesRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<FileInfoDto>>>(request);
        return new ListAllFilesResponse() { Files = response.Items };
    }

    [Action("List project target files", Description = "List target source files")]
    public async Task<ListAllFilesResponse> ListTargetFiles([ActionParameter] ListSourceFilesRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files?fields=latestVersion,name", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<FileInfoDto>>>(request);
        return new ListAllFilesResponse() { Files = response.Items };
    }

    [Action("Get source file info", Description = "Get source file info")]
    public async Task<FileInfoDto?> GetSourceFile([ActionParameter] GetFileRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files/{input.FileId}", Method.Get);
        return await Client.ExecuteWithErrorHandling<FileInfoDto>(request);
    }

    [Action("Get target file info", Description = "Get target file info")]
    public async Task<GetTargetFileInfoResponse?> GetTargetFile([ActionParameter] GetFileRequest input)
    {
        var fields = new string[] { "name", "languageDirection", "latestVersion", "analysisStatistics", "status" };
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}?fields={string.Join(", ", fields)}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<TargetFileInfoDto>(request);
        return new GetTargetFileInfoResponse(response);
    }

    [Action("Upload source file", Description = "Upload source file to project")]
    public async Task<UploadFileResponse> UploadSourceFile([ActionParameter] UploadFileRequest input)
    {
        var sourceLanguage = "";
        if (input.SourceLanguageCode is null)
        {
            var projectrequest = new LanguageCloudRequest($"/projects/{input.ProjectId}?fields=" +
            $"id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get);
            sourceLanguage = (await Client.ExecuteWithErrorHandling<ProjectDto>(projectrequest)).LanguageDirections.FirstOrDefault()?.SourceLanguage.LanguageCode;
        }
        else 
        {
            sourceLanguage = input.SourceLanguageCode;
        }
        input.File.Name = input.File.Name.Substring(input.File.Name.LastIndexOf('\\') + 1);

        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files", Method.Post);
        request.AddParameter("properties", JsonConvert.SerializeObject(new
        {
            name = input.File.Name,
            role = input.Role ?? "translatable",
            type = input.FileType ?? "native",
            language = sourceLanguage
        }), ParameterType.RequestBody);

        var fileBytes = fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("file", fileBytes, input.File.Name);
        var response = await Client.ExecuteWithErrorHandling<UploadFileResponse>(request);
        return response;
    }

    [Action("Upload zip archive", Description = "Upload zip archive with source files")]
    public async Task<ImportZipDto> UploadZipArchive([ActionParameter] UploadZipRequest input)
    {
        
        var request = new LanguageCloudRequest($"/files", Method.Post);
        var fileBytes = await fileManagementClient.DownloadAsync(input.File).Result.GetByteData();
        request.AddFile("file", fileBytes, input.File.Name);
        var importOperation = await Client.ExecuteWithErrorHandling<ZipFileStatusDto>(request);
        var pollingResult = await Client.PollImportZipArchiveOperation(importOperation.Id);
        return pollingResult;
    }

    [Action("Download target file", Description = "Download target file by id")]
    public async Task<DownloadTargetFileResponse> DownloadTargetFile([ActionParameter] DownloadFileRequest input)
    {
        var fields = new string[] { "name","latestVersion" };
        RestResponse fileData;
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}?fields={string.Join(", ", fields)}", Method.Get);
        var targetFile = await Client.ExecuteWithErrorHandling<FileInfoDto>(request);
        var format = input.Format == null ? targetFile.LatestVersion.Type : input.Format;
        if (format == "native" || format == "bcm")
        {
            var downloadRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/download",
            Method.Get);
            fileData = await Client.ExecuteWithErrorHandling(downloadRequest);

        }
        else 
        {
            var exportRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/exports?format={format}", Method.Post);
            var exportOperation = await Client.ExecuteWithErrorHandling<ExportTargetVersionDto>(exportRequest);
            var pollingResult = await Client.PollTargetFileExportOperation(exportOperation.Id, targetFile.LatestVersion.Id, input.ProjectId, input.FileId);
            var downloadRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/exports/{pollingResult.Id}/download", Method.Get);
            fileData = await Client.ExecuteWithErrorHandling(downloadRequest);
        }

        var filename = Regex.Match(
            fileData.ContentHeaders.FirstOrDefault(x => x.Name == "Content-Disposition").Value.ToString(), "filename=\"(.*?)\"").Groups[1].Value;
        using var stream = new MemoryStream(fileData.RawBytes);
        var file = await fileManagementClient.UploadAsync(stream, fileData.ContentType, String.IsNullOrEmpty(targetFile.Name) ? targetFile.Name : HttpUtility.UrlDecode(filename));
        return new DownloadTargetFileResponse()
        {
            File = file
        };
    }

    [Action("Attach source file to project", Description = "Attach source file to project")]
    public async Task AttachSourceFile([ActionParameter] AttachSourceFileRequest input)
    {
        
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files/attach-files", Method.Post);
        request.AddJsonBody(new
        {
            sourceFilesAttachment = new[]
            {
                new
                {
                    name = input.Name,
                    role = "translatable",
                    fileUrl = input.FileId,
                    type = "native",
                    language = new { 
                        languageCode = input.LanguageCode
                    }
                }
            }
        });
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update target file from SDLXLIFF", Description = "Creates a new version of a target file")]
    public async Task<UpdateTargetFileResponse> UpdateTargetFile([ActionParameter] UpdateTargetRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/imports", Method.Post);
        var fileBytes = await fileManagementClient.DownloadAsync(input.File).Result.GetByteData();
        request.AddFile("file", fileBytes, input.File.Name);
        var importOperation = await Client.ExecuteWithErrorHandling<UpdateFileImportDto>(request);
        var pollingResult = await Client.PollTargetFileVersionImport(importOperation.Id, input.ProjectId, input.FileId);
        if (pollingResult.Status == "failed") 
        {
            throw new PluginApplicationException(pollingResult.errorMessage);
        }
        
        return new UpdateTargetFileResponse {ImportStatus = pollingResult.Status, FileVersionId = pollingResult.fileVersionId };
    }
}