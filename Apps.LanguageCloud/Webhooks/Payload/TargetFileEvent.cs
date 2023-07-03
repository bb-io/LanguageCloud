namespace Apps.LanguageCloud.Webhooks.Payload
{
    public class TargetFileEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public ProjectObj Project { get; set; }
    }
}
