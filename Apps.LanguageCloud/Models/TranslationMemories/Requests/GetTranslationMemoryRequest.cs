using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.TranslationMemories.Requests;

public class GetTranslationMemoryRequest
{
    [Display("Translation Memory ID")]
    public string TranslationMemoryId { get; set; }
}