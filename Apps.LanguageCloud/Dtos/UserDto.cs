using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class UserDto
{
    [Display("User ID")]
    public string Id { get; set; }
    
    [Display("Email")]
    public string Email { get; set; }

    [Display("First name")]
    public string FirstName { get; set; }

    [Display("Last name")]
    public string LastName { get; set; }
}