using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class CompleteTaskRequest
{
    [DataSource(typeof(TaskDataHandler))]
    public string Task { get; set; }

    public string Comment { get; set; }
}