using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.ProjectTemplates.Responses;

public class ListAllProjectsTemplatesResponse
{
    public IEnumerable<ProjectTemplateDto> ProjectTemplates { get; set; }
}