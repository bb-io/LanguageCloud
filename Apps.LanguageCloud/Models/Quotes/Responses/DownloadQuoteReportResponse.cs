using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;



namespace Apps.LanguageCloud.Models.Quotes.Responses;

public class DownloadQuoteReportResponse
{
    [Display("File")]
    public FileReference File { get; set; }
}