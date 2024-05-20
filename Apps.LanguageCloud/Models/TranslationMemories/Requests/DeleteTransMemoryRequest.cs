using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.TranslationMemories.Requests;

public class DeleteTransMemoryRequest
{
    [Display("Translation Memory UID")]
    public string TranslationMemoryUId { get; set; }
}