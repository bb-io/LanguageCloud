using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.TranslationMemories.Requests;

public class InsertSegmentRequest
{
    [Display("Translation Memory UID")]
    public string TranslationMemoryUId { get; set; }

    [Display("Target Language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Source Segment")]
    public string SourceSegment { get; set; }

    [Display("Target Segment")]
    public string TargetSegment { get; set; }
}