using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.TranslationMemories.Requests;

public class InsertSegmentRequest
{
    public string TranslationMemoryUId { get; set; }

    [Display("Target Language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    public string SourceSegment { get; set; }

    public string TargetSegment { get; set; }
}