namespace Apps.LanguageCloud.Webhooks.Payload;

public class SourceFileEvent
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public ProjectObj Project { get; set; }
}

public class ProjectObj
{
    public string Id { get; set; }
}