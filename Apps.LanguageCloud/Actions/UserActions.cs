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
public class UserActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("List all users", Description = "List all users")]
    public async Task<ListAllUsersResponse> ListAllUsers()
    {
        var request = new LanguageCloudRequest("/users", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<UserDto>>>(request);
        return new ListAllUsersResponse()
        {
            Users = response.Items.Where(x => !string.IsNullOrEmpty(x.Email))
        };
    }

    [Action("Get user", Description = "Get user by ID")]
    public async Task<UserDto?> GetUser([ActionParameter] GetUserRequest input)
    {
        var request = new LanguageCloudRequest($"/users/{input.UserId}", Method.Get);
        return await Client.ExecuteWithErrorHandling<UserDto>(request);
    }
}