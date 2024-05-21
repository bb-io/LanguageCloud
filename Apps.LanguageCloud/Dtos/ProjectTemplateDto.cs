using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class ProjectTemplateDto
{
    [Display("Template ID")]
    public string Id { get; set; }

    [Display("Template Name")]
    public string Name { get; set; }
    public string Description { get; set; }
}