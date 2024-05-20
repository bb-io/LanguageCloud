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
public class GroupActions : LanguageCloudInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public GroupActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

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

    [Action("Get group", Description = "Get group by Id")]
    public GroupDto? GetGroups([ActionParameter] GetCustomerRequest input)
    {
        var request = new LanguageCloudRequest($"/groups/{input.Id}", Method.Get, Creds);
        return Client.Get<GroupDto>(request);
    }
}