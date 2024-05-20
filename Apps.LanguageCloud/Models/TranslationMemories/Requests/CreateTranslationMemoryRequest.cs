using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.TranslationMemories.Requests;

public class CreateTranslationMemoryRequest
{
    [Display("Translation Memory Name")]
    public string Name { get; set; }

    [Display("Language Processing Rule ID")]
    public string LanguageProcessingRuleId { get; set; }

    [Display("Field Template ID")]
    public string FieldTemplateId { get; set; }

    [Display("Source Language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target Language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }
}