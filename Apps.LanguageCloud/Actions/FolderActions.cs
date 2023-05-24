using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Folders.Requests;
using Apps.LanguageCloud.Models.Folders.Responses;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Users.Requests;
using Apps.LanguageCloud.Models.Users.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Actions
{
    [ActionList]
    public class FolderActions
    {
        [Action("List all folders", Description = "List all folders")]
        public ListAllFoldersResponse ListAllFolders(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest("/folders", Method.Get, authenticationCredentialsProviders);
            var response = client.Get<ResponseWrapper<List<FolderDto>>>(request);
            return new ListAllFoldersResponse()
            {
                Folders = response.Items
            };
        }

        [Action("Get folder", Description = "Get folder by Id")]
        public FolderDto? GetFolder(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetFolderRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/folders/{input.FolderId}", Method.Get, authenticationCredentialsProviders);
            return client.Get<FolderDto>(request);
        }
    }
}
