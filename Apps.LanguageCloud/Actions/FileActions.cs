using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using Apps.LanguageCloud.Models.Files.Responses;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Files.Requests;
using Blackbird.Applications.Sdk.Common.Actions;

namespace Apps.LanguageCloud.Actions
{
    [ActionList]
    public class FileActions
    {
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
            var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/target-files/{input.FileId}", Method.Get, authenticationCredentialsProviders);
            return client.Get<FileInfoDto>(request);
        }

        [Action("Upload source file", Description = "Upload source file to project")]
        public FileInfoDto UploadFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] UploadFileRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/source-files", Method.Post, authenticationCredentialsProviders);
            request.AddParameter("properties", new
            {
                name = input.FileName,
                role = "translatable",
                type = "native",
                language = input.SourceLanguageCode
            }, ParameterType.RequestBody);
            request.AddFile("file", input.File, input.FileName);
            var response = client.Execute<FileInfoDto>(request).Data;
            return response;
        }

    }
}
