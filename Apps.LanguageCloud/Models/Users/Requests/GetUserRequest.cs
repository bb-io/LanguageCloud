using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Users.Requests;

public class GetUserRequest
{
    [Display("User ID")]
    public string UserId { get; set; }
}