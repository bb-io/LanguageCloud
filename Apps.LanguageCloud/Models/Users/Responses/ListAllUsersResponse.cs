using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Users.Responses;

public class ListAllUsersResponse
{
    [Display("Users")]
    public IEnumerable<UserDto> Users { get; set; }
}