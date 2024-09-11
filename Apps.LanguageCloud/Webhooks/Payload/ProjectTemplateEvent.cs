using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Webhooks.Payload;

public class ProjectTemplateEvent
{
    [Display("Project template ID")]
    public string Id { get; set; }
    
    [Display("Project template name")]
    public string Name { get; set; }
    
    public string Description { get; set; }
}