using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.TranslationMemories.Responses;

public class ExportTransMemoryRequest
{
    [Display("Translation Memory UID")]
    public string TranslationMemoryUId { get; set; }

    [Display("File format")]
    public string FileFormat { get; set; } //"TMX" "XLSX"
}