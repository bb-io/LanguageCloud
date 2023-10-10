using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class CreateFromTemplateRequest
{
    public string Name { get; set; }


    [DataSource(typeof(ProjectTemplateDataHandler))]
    public string Template { get; set; }
}