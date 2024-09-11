using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectTemplates.Responses;

public class ListAllProjectsTemplatesResponse
{
    [Display("Project templates")]
    public IEnumerable<ProjectTemplateDto> ProjectTemplates { get; set; }
}