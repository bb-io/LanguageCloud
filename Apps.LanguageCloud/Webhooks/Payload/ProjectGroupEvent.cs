using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Webhooks.Payload;

public class ProjectGroupEvent
{
    [Display("Project group ID")]
    public string Id { get; set; }
    
    public string Action { get; set; }
    
    public List<ProjectObj> Projects { get; set; }
}