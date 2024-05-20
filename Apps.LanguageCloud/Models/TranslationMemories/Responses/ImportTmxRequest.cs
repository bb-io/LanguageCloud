using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;


namespace Apps.LanguageCloud.Models.TranslationMemories.Responses;

public class ImportTmxRequest
{
    [Display("Translation memory ID")]
    public string TranslationMemoryId { get; set; }

    public FileReference File { get; set; }

    [Display("Source Language")]
    public string SourceLanguage { get; set; }

    [Display("Target Language")]
    public string TargetLanguage { get; set; }
}