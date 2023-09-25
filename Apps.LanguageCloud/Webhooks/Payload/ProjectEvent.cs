namespace Apps.LanguageCloud.Webhooks.Payload;

public class ProjectEvent
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CustomField> CustomFields { get; set; }
}

public class CustomField
{
    public string Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}