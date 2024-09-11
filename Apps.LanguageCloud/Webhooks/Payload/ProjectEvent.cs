using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Webhooks.Payload;

public class ProjectEvent
{
    [Display("Project ID")]
    public string Id { get; set; }
    
    [Display("Project name")]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [Display("Custom fields")]
    public List<CustomField> CustomFields { get; set; }
}

public class CustomField
{
    [Display("Custom field ID")]
    public string Id { get; set; }
    
    public string Key { get; set; }
    
    public string Value { get; set; }
}