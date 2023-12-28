using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;


namespace Apps.LanguageCloud.Models.TranslationMemories.Responses;

public class ImportTmxRequest
{
    [Display("Translation memory ID")]
    public string TranslationMemoryId { get; set; }

    [Display("File")]
    public FileReference File { get; set; }

    public string SourceLanguage { get; set; }

    public string TargetLanguage { get; set; }
}