using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Users.Responses;

public class ListAllUsersResponse
{
    public IEnumerable<UserDto> Users { get; set; }
}