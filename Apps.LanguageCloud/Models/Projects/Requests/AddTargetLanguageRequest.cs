namespace Apps.LanguageCloud.Models.Projects.Requests
{
    public class AddTargetLanguageRequest
    {
        public string ProjectId { get; set; }

        public IEnumerable<string> TargetLanguages { get; set; }
    }
}
