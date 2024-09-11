using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Webhooks.Payload;

public class SourceFileEvent
{
    [Display("Source file ID")]
    public string Id { get; set; }
    
    [Display("Source file name")]
    public string Name { get; set; }
    
    public string Role { get; set; }
    
    public ProjectObj Project { get; set; }
}

public class ProjectObj
{
    [Display("Project ID")]
    public string Id { get; set; }
}