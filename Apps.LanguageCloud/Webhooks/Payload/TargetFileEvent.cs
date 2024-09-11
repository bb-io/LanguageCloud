using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Webhooks.Payload;

public class TargetFileEvent
{
    [Display("Target file ID")]
    public string Id { get; set; }
    
    [Display("Target file name")]
    public string Name { get; set; }
    
    public string Role { get; set; }
    
    public ProjectObj Project { get; set; }
}