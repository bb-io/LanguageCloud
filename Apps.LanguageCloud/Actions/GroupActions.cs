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
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    [Action("List all groups", Description = "List all groups")]
    public ListAllGroupsResponse ListAllGroups()
    {
        var request = new LanguageCloudRequest("/groups", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<GroupDto>>>(request);
        return new ListAllGroupsResponse()
        {
            Groups = response.Items
        };
    }

    [Action("Get group", Description = "Get group by ID")]
    public GroupDto? GetGroups([ActionParameter] GetCustomerRequest input)
    {
        var request = new LanguageCloudRequest($"/groups/{input.Id}", Method.Get, Creds);
        return Client.Get<GroupDto>(request);
    }
}