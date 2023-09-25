namespace Apps.LanguageCloud.Webhooks.Payload;

public class TaskEvent
{
    public string Id { get; set; }
    public string Status { get; set; }
    public string Outcome { get; set; }
    public TaskType TaskType { get; set; }
    public Project Project { get; set; }
    public Owner Owner { get; set; }
    public Location Location { get; set; }
    public List<Assignee> Assignees { get; set; }
    public DateTime DueBy { get; set; }
}

public class Assignee
{
    public string Type { get; set; }
    public User User { get; set; }
    public Group Group { get; set; }
    public VendorOrderTemplate VendorOrderTemplate { get; set; }
}

public class Group
{
}

public class Location
{
    public string Id { get; set; }
}

public class Owner
{
    public string Id { get; set; }
}

public class Project
{
    public string Id { get; set; }
}

public class TaskType
{
    public string Id { get; set; }
    public string Key { get; set; }
    public bool Automatic { get; set; }
}

public class User
{
}

public class VendorOrderTemplate
{
}