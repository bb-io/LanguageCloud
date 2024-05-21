using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Users.Requests;
using Apps.LanguageCloud.Models.Users.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class UserActions : LanguageCloudInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public UserActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    [Action("List all users", Description = "List all users")]
    public ListAllUsersResponse ListAllUsers()
    {
        var request = new LanguageCloudRequest("/users", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<UserDto>>>(request);
        return new ListAllUsersResponse()
        {
            Users = response.Items.Where(x => !string.IsNullOrEmpty(x.Email))
        };
    }

    [Action("Get user", Description = "Get user by Id")]
    public UserDto? GetUser([ActionParameter] GetUserRequest input)
    {
        var request = new LanguageCloudRequest($"/users/{input.UserId}", Method.Get, Creds);
        return Client.Get<UserDto>(request);
    }
}