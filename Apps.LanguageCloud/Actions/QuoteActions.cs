using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Quotes.Requests;
using Apps.LanguageCloud.Models.Quotes.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System.Net.Mime;
using File = Blackbird.Applications.Sdk.Common.Files.File;


namespace Apps.LanguageCloud.Actions
{
    [ActionList]
    public class QuoteActions
    {
        [Action("Download quote report", Description = "Download quote report for project")]
        public DownloadQuoteReportResponse DownloadQuoteReport(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] DownloadQuoteReportRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var exportRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/quote-report/export?format={input.FileFormat}&languageId={input.LanguageCode}",
                Method.Post, authenticationCredentialsProviders);
            var exportResult = client.Execute<ExportQuoteReportDto>(exportRequest).Data;
            client.PollQuoteReportExportOperation(exportResult.Id, input.ProjectId, input.FileFormat, authenticationCredentialsProviders);
            var downloadRequest = new LanguageCloudRequest($"/projects/{input.ProjectId}/quote-report/download?format={input.FileFormat}&exportId={exportResult.Id}",
                Method.Get, authenticationCredentialsProviders);
            var fileData = client.Get(downloadRequest).RawBytes;
            var fileFormat = input.FileFormat == "pdf" ? "pdf" : "xlsx";
            return new DownloadQuoteReportResponse()
            {
                File = new File(fileData) {
                    Name = $"QuoteReport_{input.LanguageCode}_{DateTime.Now.ToString("yyyyMMddTHHmmss")}.{fileFormat}",
                    ContentType = MediaTypeNames.Application.Octet
                }
            };
        }
    }
}
