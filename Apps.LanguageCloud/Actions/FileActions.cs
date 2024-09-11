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

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class FileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : LanguageCloudInvocable(invocationContext)
{
    [Action("List project source files", Description = "List project source files")]
    public ListAllFilesResponse ListSourceFiles([ActionParameter] ListSourceFilesRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<FileInfoDto>>>(request);
        return new ListAllFilesResponse() { Files = response.Items };
    }

    [Action("List project target files", Description = "List target source files")]
    public ListAllFilesResponse ListTargetFiles([ActionParameter] ListSourceFilesRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files?fields=latestVersion", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<FileInfoDto>>>(request);
        return new ListAllFilesResponse() { Files = response.Items };
    }

    [Action("Get source file info", Description = "Get source file info")]
    public FileInfoDto? GetSourceFile([ActionParameter] GetFileRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files/{input.FileId}", Method.Get, Creds);
        return Client.Get<FileInfoDto>(request);
    }

    [Action("Get target file info", Description = "Get target file info")]
    public GetTargetFileInfoResponse? GetTargetFile([ActionParameter] GetFileRequest input)
    {
        var fields = new string[] { "name", "languageDirection", "latestVersion", "analysisStatistics", "status" };
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}?fields={string.Join(", ", fields)}", Method.Get, Creds);
        var response = Client.Get<TargetFileInfoDto>(request);
        return new GetTargetFileInfoResponse(response);
    }

    [Action("Upload source file", Description = "Upload source file to project")]
    public UploadFileResponse UploadSourceFile([ActionParameter] UploadFileRequest input)
    {
        var sourceLanguage = "";
        if (input.SourceLanguageCode is null)
        {
            var projectrequest = new LanguageCloudRequest($"/projects/{input.ProjectId}?fields=" +
            $"id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get, Creds);
            sourceLanguage = Client.Get<ProjectDto>(projectrequest).LanguageDirections.FirstOrDefault().SourceLanguage.LanguageCode;
        }
        else 
        {
            sourceLanguage = input.SourceLanguageCode;
        }
        input.File.Name = input.File.Name.Substring(input.File.Name.LastIndexOf('\\') + 1);

        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files", Method.Post, Creds);
        request.AddParameter("properties", JsonConvert.SerializeObject(new
        {
            name = input.File.Name,
            role = input.Role ?? "translatable",
            type = input.FileType ?? "native",
            language = sourceLanguage
        }), ParameterType.RequestBody);

        var fileBytes = fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("file", fileBytes, input.File.Name);
        var response = Client.Execute<UploadFileResponse>(request).Data;
        return response;
    }

    [Action("Upload zip archive", Description = "Upload zip archive with source files")]
    public ImportZipDto UploadZipArchive([ActionParameter] UploadZipRequest input)
    {
        
        var request = new LanguageCloudRequest($"/files", Method.Post, Creds);
        var fileBytes = fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("file", fileBytes, input.File.Name);
        var importOperation = Client.Execute<ZipFileStatusDto>(request).Data;
        var pollingResult = Client.PollImportZipArchiveOperation(importOperation.Id, Creds);
        return pollingResult;
    }

    [Action("Download target file", Description = "Download target file by id")]
    public async Task<DownloadTargetFileResponse> DownloadTargetFile([ActionParameter] DownloadFileRequest input)
    {
        
        byte[] fileData;
        var targetFile = GetTargetFile(new GetFileRequest() { ProjectId = input.ProjectId, FileId = input.FileId });
        var format = input.Format == null ? targetFile.LatestVersion.Type : input.Format;
        if (format == "native" || format == "bcm")
        {
            var downloadRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/download",
            Method.Get, Creds);
            fileData = Client.Get(downloadRequest).RawBytes;

        }
        else 
        {
            var exportRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/exports?format={format}",
            Method.Post, Creds);
            var exportOperation = Client.Execute<ExportTargetVersionDto>(exportRequest).Data;
            var pollingResult = Client.PollTargetFileExportOperation(exportOperation.Id, targetFile.LatestVersion.Id, input.ProjectId, input.FileId, Creds);
            var downloadRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/exports/{pollingResult.Id}/download",
                Method.Get, Creds);
            fileData = Client.Get(downloadRequest).RawBytes;
        }
        
        using var stream = new MemoryStream(fileData);
        var file = await fileManagementClient.UploadAsync(stream, MediaTypeNames.Application.Octet, targetFile.Name);
        return new DownloadTargetFileResponse()
        {
            File = file
        };
    }

    [Action("Attach source file to project", Description = "Attach source file to project")]
    public void AttachSourceFile([ActionParameter] AttachSourceFileRequest input)
    {
        
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files/attach-files", Method.Post, Creds);
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
        Client.Execute(request);
    }

    [Action("Update target file from SDLXLIFF", Description = "Creates a new version of a target file")]
    public UpdateTargetFileResponse UpdateTargetFile([ActionParameter] UpdateTargetRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/imports", Method.Post, Creds);
        var fileBytes = fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("file", fileBytes, input.File.Name);
        var importOperation = Client.Execute<UpdateFileImportDto>(request).Data;
        var pollingResult = Client.PollTargetFileVersionImport(importOperation.Id, input.ProjectId, input.FileId, Creds);
        if (pollingResult.Status == "failed") 
        {
            throw new Exception(pollingResult.errorMessage);
        }
        
        return new UpdateTargetFileResponse {ImportStatus = pollingResult.Status, FileVersionId = pollingResult.fileVersionId };
    }
}