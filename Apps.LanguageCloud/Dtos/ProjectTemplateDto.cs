using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class ProjectTemplateDto
{
    [Display("Template ID")]
    public string Id { get; set; }

    [Display("Template name")]
    public string Name { get; set; }
    
    [Display("Description")]
    public string Description { get; set; }
}