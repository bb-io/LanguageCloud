using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.LanguageCloud;

public class LanguageCloudClient : RestClient
{
    public LanguageCloudClient() : 
        base(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = new Uri("https://lc-api.sdl.com/public-api/v1/") }) { }


    public ExportTargetVersionDto PollTargetFileExportOperation(string exportId, string fileVersionId, string projectId, string targetFileId, 
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var request = new LanguageCloudRequest($"/projects/{projectId}/target-files/{targetFileId}/versions/{fileVersionId}/exports/{exportId}",
            Method.Get, authenticationCredentialsProviders);
        var response = this.Get<ExportTargetVersionDto>(request);
        while (response?.Status == "inProgress")
        {
            Task.Delay(2000);
            response = this.Get<ExportTargetVersionDto>(request);
        }
        if(response?.Status != "completed")
        {
            throw new Exception(response?.ErrorMessage);
        }
        return response;
    }

    public PollExportQuoteReportDto PollQuoteReportExportOperation(string exportId, string projectId, string format,
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var request = new LanguageCloudRequest($"/projects/{projectId}/quote-report/export?format={format}&exportId={exportId}",
            Method.Get, authenticationCredentialsProviders);
        var response = this.Get<PollExportQuoteReportDto>(request);
        while (response?.Status == "inProgress")
        {
            Task.Delay(2000);
            response = this.Get<PollExportQuoteReportDto>(request);
        }
        return response;
    }

    public ImportTmxDto PollImportTMXOperation(string importId,
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var request = new LanguageCloudRequest($"/translation-memory/imports/{importId}",
            Method.Get, authenticationCredentialsProviders);
        Task.Delay(2000);
        var response = this.Get<ImportTmxDto>(request);
        while (response?.Status == "inProgress" || response?.Status == "queued")
        {
            Task.Delay(2000);
            response = this.Get<ImportTmxDto>(request);
        }
        return response;
    }

    public UpdateFileImportDto PollTargetFileVersionImport(string importId, string projectId, string targetFileId,
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var request = new LanguageCloudRequest($"/projects/{projectId}/target-files/{targetFileId}/versions/imports/{importId}",
            Method.Get, authenticationCredentialsProviders);
        Task.Delay(2000);
        var response = this.Get<UpdateFileImportDto>(request);
        while (response?.Status == "inProgress" || response?.Status == "created")
        {
            Task.Delay(2000);
            response = this.Get<UpdateFileImportDto>(request);
        }
        return response;
    }

    public ImportZipDto PollImportZipArchiveOperation(string fileId,
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var request = new LanguageCloudRequest($"/files/{fileId}",
            Method.Get, authenticationCredentialsProviders);
        var response = this.Get<ImportZipDto>(request);
        while (response?.UnzipStatus == "extracting" || response?.UnzipStatus == "queued")
        {
            Task.Delay(2000);
            response = this.Get<ImportZipDto>(request);
        }
        return response;
    }

    public ImportTmxDto PollImportGlossariesOperation(string importId, string termbaseId,
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var request = new LanguageCloudRequest($"/termbases/{termbaseId}/imports/{importId}",
            Method.Get, authenticationCredentialsProviders);
        Task.Delay(2000);
        var response = this.Get<ImportTmxDto>(request);
        while (response?.Status == "processing" || response?.Status == "queued")
        {
            Task.Delay(2000);
            response = this.Get<ImportTmxDto>(request);
        }
        return response;
    }

    public ExportTargetVersionDto PollExportGlossariesOperation(string exportId, string termbaseId,
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var request = new LanguageCloudRequest($"/termbases/{termbaseId}/exports/{exportId}",
            Method.Get, authenticationCredentialsProviders);
        Task.Delay(2000);
        var response = this.Get<ExportTargetVersionDto>(request);
        while (response?.Status == "processing" || response?.Status == "queued")
        {
            Task.Delay(2000);
            response = this.Get<ExportTargetVersionDto>(request);
        }
        if (response?.Status != "done")
        {
            throw new Exception(response?.ErrorMessage);
        }
        return response;
    }
}