namespace Apps.LanguageCloud.Models.TranslationMemories.Requests
{
    public class CreateTranslationMemoryRequest
    {
        public string Name { get; set; }

        public string LanguageProcessingRuleId { get; set; }

        public string FieldTemplateId { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }
    }
}
