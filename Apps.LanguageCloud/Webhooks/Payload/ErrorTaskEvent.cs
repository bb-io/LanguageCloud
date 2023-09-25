namespace Apps.LanguageCloud.Webhooks.Payload;

public class ErrorTaskEvent
{
    public string Id { get; set; }
    public string Status { get; set; }
    public string Outcome { get; set; }
    public TaskType TaskType { get; set; }
    public ProjectError Project { get; set; }
    public Owner Owner { get; set; }
    public Location Location { get; set; }
    public List<Assignee> Assignees { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CompletedAt { get; set; }
    public DateTime DueBy { get; set; }
    public FailedTask FailedTask { get; set; }
}

public class Error
{
}

public class FailedTask
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Error> Errors { get; set; }
    public FailedTaskType FailedTaskType { get; set; }
}

public class FailedTaskType
{
    public string Id { get; set; }
    public string Key { get; set; }
}

public class ProjectError
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}