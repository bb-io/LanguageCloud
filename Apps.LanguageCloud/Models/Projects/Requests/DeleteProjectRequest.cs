using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class DeleteProjectRequest
{
    [DataSource(typeof(ProjectDataHandler))]
    public string Project { get; set; }
}