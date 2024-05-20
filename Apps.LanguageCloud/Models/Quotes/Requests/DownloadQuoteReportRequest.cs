using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Quotes.Requests;

public class DownloadQuoteReportRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string Project { get; set; }

    [Display("Language code")]
    [DataSource(typeof(LanguageDataHandler))]
    public string LanguageCode { get; set; }

    [Display("File format")]
    [DataSource(typeof(QuoteFormatDataHandler))]
    public string FileFormat { get; set; } // pdf or excel
}