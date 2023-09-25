namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class AssignTaskRequest
{
    public string TaskId { get; set; }
    public string AssigneeId { get; set; }

    public string Type { get; set; }
}