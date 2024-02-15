using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Quotes.Requests;

public class DownloadQuoteReportRequest
{
    [DataSource(typeof(ProjectDataHandler))]
    public string Project { get; set; }

    [Display("Language code")]
    [DataSource(typeof(LanguageDataHandler))]
    public string LanguageCode { get; set; }

    [DataSource(typeof(QuoteFormatDataHandler))]
    public string FileFormat { get; set; } // pdf or excel
}