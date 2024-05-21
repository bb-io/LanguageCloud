using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Folders.Requests;
using Apps.LanguageCloud.Models.Folders.Responses;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class FolderActions : LanguageCloudInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public FolderActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List all folders", Description = "List all folders")]
    public ListAllFoldersResponse ListAllFolders()
    {
        var request = new LanguageCloudRequest("/folders", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<FolderDto>>>(request);
        return new ListAllFoldersResponse()
        {
            Folders = response.Items
        };
    }

    [Action("Get folder", Description = "Get folder by Id")]
    public FolderDto? GetFolder([ActionParameter] GetFolderRequest input)
    {
        var Client = new LanguageCloudClient();var request = new LanguageCloudRequest($"/folders/{input.FolderId}", Method.Get, Creds);
        return Client.Get<FolderDto>(request);
    }
}