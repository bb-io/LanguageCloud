using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class GroupDto
{
    [Display("Group ID")]
    public string Id { get; set; }
    
    [Display("Group name")]
    public string Name { get; set; }
}