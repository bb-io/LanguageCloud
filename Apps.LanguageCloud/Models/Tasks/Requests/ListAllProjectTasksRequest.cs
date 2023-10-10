using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class ListAllProjectTasksRequest
{
    [DataSource(typeof(ProjectDataHandler))]
    public string Project { get; set; }
}