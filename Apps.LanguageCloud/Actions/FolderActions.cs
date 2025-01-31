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
public class FolderActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("List all folders", Description = "List all folders")]
    public async Task<ListAllFoldersResponse> ListAllFolders()
    {
        var request = new LanguageCloudRequest("/folders", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<FolderDto>>>(request);
        return new ListAllFoldersResponse()
        {
            Folders = response.Items
        };
    }

    [Action("Get folder", Description = "Get folder by Id")]
    public async Task<FolderDto?> GetFolder([ActionParameter] GetFolderRequest input)
    {
        var request = new LanguageCloudRequest($"/folders/{input.FolderId}", Method.Get);
        return await Client.ExecuteWithErrorHandling<FolderDto>(request);
    }
}