using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Customers.Requests;
using Apps.LanguageCloud.Models.Groups.Responses;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class GroupActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("List all groups", Description = "List all groups")]
    public async Task<ListAllGroupsResponse> ListAllGroups()
    {
        var request = new LanguageCloudRequest("/groups", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<GroupDto>>>(request);
        return new ListAllGroupsResponse()
        {
            Groups = response.Items
        };
    }

    [Action("Get group", Description = "Get group by ID")]
    public async Task<GroupDto?> GetGroups([ActionParameter] GetCustomerRequest input)
    {
        var request = new LanguageCloudRequest($"/groups/{input.Id}", Method.Get);
        return await Client.ExecuteWithErrorHandling<GroupDto>(request);
    }
}