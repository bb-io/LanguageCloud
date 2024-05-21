using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class UserDto
{
    [Display("User ID")]
    public string Id { get; set; }
    public string Email { get; set; }

    [Display("First Name")]
    public string FirstName { get; set; }

    [Display("Last Name")]
    public string LastName { get; set; }
}