using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RestSharp;
using DocumentFormat.OpenXml.Drawing;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;

namespace Apps.LanguageCloud;

public class LanguageCloudClient : BlackBirdRestClient
{
    public LanguageCloudClient(IEnumerable<AuthenticationCredentialsProvider> creds) : 
        base(new RestClientOptions() { BaseUrl = new Uri("https://lc-api.sdl.com/public-api/v1/") }) 
    {

        var authCreds = GetAuthCreds(creds);
        this.AddDefaultHeader("Authorization", $"Bearer {authCreds.AccessToken}");
        this.AddDefaultHeader("X-LC-Tenant", creds.First(p => p.KeyName == "tenantId").Value);
        this.AddDefaultHeader("accept", "*/*");
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return new PluginMisconfigurationException("The connection is unauthorized. Please check your Tenant ID in the connection settings.");
        }
        try
        {
            var error = JsonConvert.DeserializeObject<ErrorDto>(response.Content);
            if (error?.ErrorCode == "invalid")
            {
                if (error.Details != null && error.Details.Select(x => x.Name).Any())
                {
                    return new PluginMisconfigurationException($"The following input parameters were invalid: {string.Join(", ", error.Details.Select(x => x.Name))}. Please reconfigure these input values.");
                }
                return new PluginMisconfigurationException("One or more of the input values are invalid. Please reconfigure the input values.");
            }
            return new PluginApplicationException(error.Message);
        }
        catch (Exception ex)
        {
            return new PluginApplicationException(response.Content);
        } 
    }


    private CredsResponse GetAuthCreds(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new RestRequest("https://sdl-prod.eu.auth0.com/oauth/token", Method.Post)
            .AddJsonBody(new
            {
                client_id = creds.Get(CredsNames.ClientId).Value,
                client_secret = creds.Get(CredsNames.ClientSecret).Value,
                grant_type = "client_credentials",
                audience = "https://api.sdl.com"
            });

        var response = new RestClient().Execute(request);

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
            {
                throw new PluginMisconfigurationException("The connection is unauthorized. Please check your Client ID and Client Secret in the connection settings.");
            }
        }

        return JsonConvert.DeserializeObject<CredsResponse>(response.Content, new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        });
    }

    public async Task<ExportTargetVersionDto> PollTargetFileExportOperation(string exportId, string fileVersionId, string projectId, string targetFileId)
    {
        var request = new LanguageCloudRequest($"/projects/{projectId}/target-files/{targetFileId}/versions/{fileVersionId}/exports/{exportId}", Method.Get);
        var response = await ExecuteWithErrorHandling<ExportTargetVersionDto>(request);
        while (response?.Status == "inProgress" || response?.Status == "created" || response?.Status == "queued")
        {
            await Task.Delay(2000);
            response = await ExecuteWithErrorHandling<ExportTargetVersionDto>(request);
        }
        if(response?.Status != "completed")
        {
            throw new PluginApplicationException(response?.ErrorMessage);
        }
        return response;
    }

    public async Task<PollExportQuoteReportDto> PollQuoteReportExportOperation(string exportId, string projectId, string format)
    {
        var request = new LanguageCloudRequest($"/projects/{projectId}/quote-report/export?format={format}&exportId={exportId}", Method.Get);
        var response = await ExecuteWithErrorHandling<PollExportQuoteReportDto>(request);
        while (response?.Status == "inProgress" || response?.Status == "created" || response?.Status == "queued")
        {
            await Task.Delay(2000);
            response = await ExecuteWithErrorHandling<PollExportQuoteReportDto>(request);
        }
        return response;
    }

    public async Task<ImportTmxDto> PollImportTMXOperation(string importId)
    {
        var request = new LanguageCloudRequest($"/translation-memory/imports/{importId}", Method.Get);
        await Task.Delay(2000);
        var response = await ExecuteWithErrorHandling<ImportTmxDto>(request);
        while (response?.Status == "inProgress" || response?.Status == "created" || response?.Status == "queued")
        {
            await Task.Delay(2000);
            response = await ExecuteWithErrorHandling<ImportTmxDto>(request);
        }
        return response;
    }

    public async Task<UpdateFileImportDto> PollTargetFileVersionImport(string importId, string projectId, string targetFileId)
    {
        var request = new LanguageCloudRequest($"/projects/{projectId}/target-files/{targetFileId}/versions/imports/{importId}", Method.Get);
        await Task.Delay(2000);
        var response = await ExecuteWithErrorHandling<UpdateFileImportDto>(request);
        while (response?.Status == "inProgress" || response?.Status == "created" || response?.Status == "queued")
        {
            await Task.Delay(2000);
            response = await ExecuteWithErrorHandling<UpdateFileImportDto>(request);
        }
        return response;
    }

    public async Task<ImportZipDto> PollImportZipArchiveOperation(string fileId)
    {
        var request = new LanguageCloudRequest($"/files/{fileId}", Method.Get);
        var response = await ExecuteWithErrorHandling<ImportZipDto>(request);
        while (response?.UnzipStatus == "extracting" || response?.UnzipStatus == "queued")
        {
            await Task.Delay(2000);
            response = await ExecuteWithErrorHandling<ImportZipDto>(request);
        }
        return response;
    }

    public async Task<ImportTmxDto> PollImportGlossariesOperation(string importId, string termbaseId)
    {
        var request = new LanguageCloudRequest($"/termbases/{termbaseId}/imports/{importId}", Method.Get);
        await Task.Delay(2000);
        var response = await ExecuteWithErrorHandling<ImportTmxDto>(request);
        while (response?.Status == "processing" || response?.Status == "queued" || response?.Status == "created")
        {
            await Task.Delay(2000);
            response = await ExecuteWithErrorHandling<ImportTmxDto>(request);
        }
        return response;
    }

    public async Task<ExportTargetVersionDto> PollExportGlossariesOperation(string exportId, string termbaseId)
    {
        var request = new LanguageCloudRequest($"/termbases/{termbaseId}/exports/{exportId}", Method.Get);
        await Task.Delay(2000);
        var response = await ExecuteWithErrorHandling<ExportTargetVersionDto>(request);
        while (response?.Status == "processing" || response?.Status == "queued" || response?.Status == "created")
        {
            await Task.Delay(2000);
            response = await ExecuteWithErrorHandling<ExportTargetVersionDto>(request);
        }
        if (response?.Status != "done")
        {
            throw new PluginApplicationException(response?.ErrorMessage);
        }
        return response;
    }
}