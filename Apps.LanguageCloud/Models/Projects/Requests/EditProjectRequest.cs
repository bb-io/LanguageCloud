using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class EditProjectRequest
{
    [DataSource(typeof(ProjectDataHandler))]
    public string Project { get; set; }

    public string ProjectName { get; set; }
}