using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class AssignTaskRequest
{
    [Display("Task ID")]
    [DataSource(typeof(TaskDataHandler))]
    public string Task { get; set; }

    [Display("Assignee ID")]
    public string AssigneeId { get; set; }

    [Display("User type")]
    [DataSource(typeof(UserTypeDataHandler))]
    public string Type { get; set; }
}