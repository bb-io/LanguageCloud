using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class GetTaskOptionalRequest
{
    [Display("Task ID")]
    [DataSource(typeof(TaskDataHandler))]
    public string? TaskId { get; set; }
}