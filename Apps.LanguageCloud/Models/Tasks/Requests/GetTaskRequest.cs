using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class GetTaskRequest
{
    [Display("Task ID")]
    public string Task { get; set; }
}