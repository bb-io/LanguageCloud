namespace Apps.LanguageCloud.Webhooks.Payload;

public class ProjectGroupEvent
{
    public string Id { get; set; }
    public string Action { get; set; }
    public List<ProjectObj> Projects { get; set; }
}