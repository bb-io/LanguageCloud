using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Groups.Requests;

public class GetGroupRequest
{
    [Display("Group ID")]
    public string Id { get; set; }
}