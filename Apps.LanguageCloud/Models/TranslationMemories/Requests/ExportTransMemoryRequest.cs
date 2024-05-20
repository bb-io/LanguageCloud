using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.LanguageCloud.Models.TranslationMemories.Responses;

public class ExportTransMemoryRequest
{
    [Display("Translation Memory UID")]
    public string TranslationMemoryUId { get; set; }

    [Display("File format")]
    [StaticDataSource(typeof(FileFormatDataHandler))]
    public string FileFormat { get; set; } //"TMX" "XLSX"
}