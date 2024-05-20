using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Quotes.Requests;
using Apps.LanguageCloud.Models.Quotes.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Net.Mime;



namespace Apps.LanguageCloud.Actions;

[ActionList]
public class QuoteActions : LanguageCloudInvocable
{
    private readonly IFileManagementClient _fileManagementClient;

    public QuoteActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(
        invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("Download quote report", Description = "Download quote report for project")]
    public async Task<DownloadQuoteReportResponse> DownloadQuoteReport([ActionParameter] DownloadQuoteReportRequest input)
    {
        var exportRequest = new LanguageCloudRequest($"/projects/{input.Project}/quote-report/export?format={input.FileFormat}&languageId={input.LanguageCode}",
            Method.Post, Creds);
        var exportResult = Client.Execute<ExportQuoteReportDto>(exportRequest).Data;
        Client.PollQuoteReportExportOperation(exportResult.Id, input.Project, input.FileFormat, Creds);
        var downloadRequest = new LanguageCloudRequest($"/projects/{input.Project}/quote-report/download?format={input.FileFormat}&exportId={exportResult.Id}",
            Method.Get, Creds);
        var fileData = Client.Get(downloadRequest).RawBytes;
        var fileFormat = input.FileFormat == "pdf" ? "pdf" : "xlsx";

        using var stream = new MemoryStream(fileData);
        var file = await _fileManagementClient.UploadAsync(stream, MediaTypeNames.Application.Octet, $"QuoteReport_{input.LanguageCode}_{DateTime.Now.ToString("yyyyMMddTHHmmss")}.{fileFormat}");
        return new DownloadQuoteReportResponse()
        {
            File = file
        };
    }
}