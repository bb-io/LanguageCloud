using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.ProjectTemplates.Requests;

public class GetProjectTemplateRequest
{
    [Display("Template ID"), DataSource(typeof(ProjectTemplateDataHandler))]
    public string Template { get; set; } 
}