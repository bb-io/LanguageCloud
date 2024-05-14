using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Customers.Requests;
using Apps.LanguageCloud.Models.Groups.Responses;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class GroupActions
{
        
    [Action("List all groups", Description = "List all groups")]
    public ListAllGroupsResponse ListAllGroups(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest("/groups", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<GroupDto>>>(request);
        return new ListAllGroupsResponse()
        {
            Groups = response.Items
        };
    }

    [Action("Get group", Description = "Get group by Id")]
    public GroupDto? GetGroups(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetCustomerRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/groups/{input.Id}", Method.Get, authenticationCredentialsProviders);
        return client.Get<GroupDto>(request);
    }
}