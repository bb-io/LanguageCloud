using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.TranslationMemories.Requests;

public class GetTranslationMemoryRequest
{
    [Display("Translation memory ID"), DataSource(typeof(TranslationMemoryDataSource))]
    public string TranslationMemoryId { get; set; } = string.Empty;
}