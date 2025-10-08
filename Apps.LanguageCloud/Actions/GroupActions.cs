using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models;
using Apps.LanguageCloud.Models.Customers.Requests;
using Apps.LanguageCloud.Models.Groups.Responses;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList("Groups")]
public class GroupActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("Search groups", Description = "Search for groups, optionally by location")]
    public async Task<ListAllGroupsResponse> SearchGroups([ActionParameter] OptionalLocationRequest location)
    {
        var request = new LanguageCloudRequest("/groups", Method.Get);
        if (location.Location != null)
            request.AddQueryParameter("location", location.Location);
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