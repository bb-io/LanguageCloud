using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Projects.Requests;
using Apps.LanguageCloud.Models.Projects.Responses;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Users.Requests;
using Apps.LanguageCloud.Models.Users.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.LanguageCloud.Actions
{
    [ActionList]
    public class UserActions
    {
        [Action("List all users", Description = "List all users")]
        public ListAllUsersResponse ListAllUsers(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest("/users", Method.Get, authenticationCredentialsProviders);
            var response = client.Get<ResponseWrapper<List<UserDto>>>(request);
            return new ListAllUsersResponse()
            {
                Users = response.Items.Where(x => !string.IsNullOrEmpty(x.Email))
            };
        }

        [Action("Get user", Description = "Get user by Id")]
        public UserDto? GetUser(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetUserRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/users/{input.UserId}", Method.Get, authenticationCredentialsProviders);
            return client.Get<UserDto>(request);
        }
    }
}
