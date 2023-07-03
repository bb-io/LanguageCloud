namespace Apps.LanguageCloud.Models.Projects.Requests
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }
    }
}
