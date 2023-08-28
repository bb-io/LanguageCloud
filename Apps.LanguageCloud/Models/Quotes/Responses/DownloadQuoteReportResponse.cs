using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;


namespace Apps.LanguageCloud.Models.Quotes.Responses
{
    public class DownloadQuoteReportResponse
    {
        [Display("File")]
        public File File { get; set; }
    }
}
