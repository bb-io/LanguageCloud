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

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class FileActions
{
    private readonly IFileManagementClient _fileManagementClient;

    public FileActions(IFileManagementClient fileManagementClient)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("List project source files", Description = "List project source files")]
    public ListAllFilesResponse ListSourceFiles(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, 
        [ActionParameter] ListSourceFilesRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<FileInfoDto>>>(request);
        return new ListAllFilesResponse() { Files = response.Items };
    }

    [Action("List project target files", Description = "List target source files")]
    public ListAllFilesResponse ListTargetFiles(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] ListSourceFilesRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<FileInfoDto>>>(request);
        return new ListAllFilesResponse() { Files = response.Items };
    }

    [Action("Get source file info", Description = "Get source file info")]
    public FileInfoDto? GetSourceFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetFileRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files/{input.FileId}", Method.Get, authenticationCredentialsProviders);
        return client.Get<FileInfoDto>(request);
    }

    [Action("Get target file info", Description = "Get target file info")]
    public FileInfoDto? GetTargetFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetFileRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var fields = new string[] { "name", "languageDirection", "latestVersion" };
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}?fields={string.Join(", ", fields)}", Method.Get, authenticationCredentialsProviders);
        return client.Get<FileInfoDto>(request);
    }

    [Action("Upload source file", Description = "Upload source file to project")]
    public UploadFileResponse UploadSourceFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UploadFileRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files", Method.Post, authenticationCredentialsProviders);
        request.AddParameter("properties", JsonConvert.SerializeObject(new
        {
            name = input.File.Name,
            role = input.Role ?? "translatable",
            type = input.FileType ?? "native",
            language = input.SourceLanguageCode
        }), ParameterType.RequestBody);

        var fileBytes = _fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("file", fileBytes, input.File.Name);
        var response = client.Execute<UploadFileResponse>(request).Data;
        return response;
    }

    [Action("Upload zip archive", Description = "Upload zip archive with source files")]
    public ImportZipDto UploadZipArchive(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UploadZipRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/files", Method.Post, authenticationCredentialsProviders);
        var fileBytes = _fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("file", fileBytes, input.File.Name);
        var importOperation = client.Execute<ZipFileStatusDto>(request).Data;
        var pollingResult = client.PollImportZipArchiveOperation(importOperation.Id, authenticationCredentialsProviders);
        return pollingResult;
    }

    [Action("Download target file", Description = "Download target file by id")]
    public async Task<DownloadTargetFileResponse> DownloadTargetFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] DownloadFileRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);

        var targetFile = GetTargetFile(authenticationCredentialsProviders, new GetFileRequest() { ProjectId = input.ProjectId, FileId = input.FileId });
        var exportRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/exports?format=native", 
            Method.Post, authenticationCredentialsProviders);
        var exportOperation = client.Execute<ExportTargetVersionDto>(exportRequest).Data;
        var pollingResult = client.PollTargetFileExportOperation(exportOperation.Id, targetFile.LatestVersion.Id, input.ProjectId, input.FileId, authenticationCredentialsProviders);
        var downloadRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}/versions/{targetFile.LatestVersion.Id}/exports/{pollingResult.Id}/download", 
            Method.Get, authenticationCredentialsProviders);
        var fileData = client.Get(downloadRequest).RawBytes;

        using var stream = new MemoryStream(fileData);
        var file = await _fileManagementClient.UploadAsync(stream, MediaTypeNames.Application.Octet, targetFile.Name);
        return new DownloadTargetFileResponse()
        {
            File = file
        };
    }

    [Action("Attach source file to project", Description = "Attach source file to project")]
    public void AttachSourceFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] AttachSourceFileRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files/attach-files", Method.Post, authenticationCredentialsProviders);
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
        client.Execute(request);
    }

}