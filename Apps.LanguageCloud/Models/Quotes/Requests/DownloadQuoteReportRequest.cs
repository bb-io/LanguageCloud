namespace Apps.LanguageCloud.Models.Quotes.Requests;

public class DownloadQuoteReportRequest
{
    public string ProjectId { get; set; }

    public string LanguageCode { get; set; }

    public string FileFormat { get; set; } // pdf or excel
}