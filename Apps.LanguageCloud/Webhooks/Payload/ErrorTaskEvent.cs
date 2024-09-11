using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Webhooks.Payload;

public class ErrorTaskEvent
{
    [Display("Error task ID")]
    public string Id { get; set; }
    
    public string Status { get; set; }
    
    public string Outcome { get; set; }
    
    [Display("Task type")]
    public TaskType TaskType { get; set; }
    
    public ProjectError Project { get; set; }
    
    public Owner Owner { get; set; }
    
    public Location Location { get; set; }
    
    public List<Assignee> Assignees { get; set; }
    
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    [Display("Completed at")]
    public DateTime CompletedAt { get; set; }
    
    [Display("Due by")]
    public DateTime DueBy { get; set; }
    
    [Display("Failed task")]
    public FailedTask FailedTask { get; set; }
}

public class Error
{
}

public class FailedTask
{
    [Display("Failed task ID")]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Error> Errors { get; set; }
    
    [Display("Failed task type")]
    public FailedTaskType FailedTaskType { get; set; }
}

public class FailedTaskType
{
    public string Id { get; set; }
    
    public string Key { get; set; }
}

public class ProjectError
{
    [Display("Project ID")]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
}

public class TaskType
{
    public string Id { get; set; }
    
    public string Key { get; set; }
    
    public bool Automatic { get; set; }
}

public class Location
{
    [Display("Location ID")]
    public string Id { get; set; }
}