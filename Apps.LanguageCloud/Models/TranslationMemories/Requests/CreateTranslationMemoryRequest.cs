using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.TranslationMemories.Requests;

public class CreateTranslationMemoryRequest
{
    [Display("Translation memory name")]
    public string Name { get; set; }

    [Display("Language processing rule ID")]
    [DataSource(typeof(LanguageProcessingRuleDataHandler))]
    public string LanguageProcessingRuleId { get; set; }

    [Display("Field template ID")]
    [DataSource(typeof(FieldTemplateDataHandler))]
    public string FieldTemplateId { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }
}