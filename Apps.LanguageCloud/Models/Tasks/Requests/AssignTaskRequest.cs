using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class AssignTaskRequest
{
    [DataSource(typeof(TaskDataHandler))]
    public string Task { get; set; }
    public string AssigneeId { get; set; }

    public string Type { get; set; }
}