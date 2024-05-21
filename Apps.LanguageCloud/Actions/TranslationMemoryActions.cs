using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.TranslationMemories.Requests;
using Apps.LanguageCloud.Models.TranslationMemories.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Json;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class TranslationMemoryActions : LanguageCloudInvocable
{
    private readonly IFileManagementClient _fileManagementClient;

    public TranslationMemoryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(
        invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("List translation memories", Description = "List translation memories")]
    public ListTranslationMemoriesResponse ListTranslationMemories()
    {
        var request = new LanguageCloudRequest($"/translation-memory", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<TranslationMemoryDto>>>(request);
        return new ListTranslationMemoriesResponse()
        {
            Memories = response.Items
        };
    }

    [Action("Create translation memory", Description = "Create translation memory")]
    public TranslationMemoryDto CreateTranslationMemory([ActionParameter] CreateTranslationMemoryRequest input)
    {
        var request = new LanguageCloudRequest($"/translation-memory", Method.Post, Creds);
        request.AddJsonBody(new
        {
            name = input.Name,
            languageDirections = new[]
            {
                new
                {
                    sourceLanguage = new {languageCode = input.SourceLanguage },
                    targetLanguage = new {languageCode = input.TargetLanguage }
                }
            },
            languageProcessingRuleId = input.LanguageProcessingRuleId,
            fieldTemplateId = input.FieldTemplateId
        });
        return Client.Execute<TranslationMemoryDto>(request).Data;
    }

    [Action("Get translation memory", Description = "Get translation memory")]
    public TranslationMemoryDto GetTranslationMemory([ActionParameter] GetTranslationMemoryRequest input)
    {
        var request = new LanguageCloudRequest($"/translation-memory/{input.TranslationMemoryId}", Method.Get, Creds);
        var response = Client.Get<TranslationMemoryDto>(request);
        return response;
    }

    [Action("Import TMX file", Description = "Import TMX file")]
    public async Task ImportTmx([ActionParameter] ImportTmxRequest input)
    {
        var restClient = new LanguageCloudClient();

        using var memoryStream = _fileManagementClient.DownloadAsync(input.File).Result;
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://lc-api.sdl.com/public-api/v1/translation-memory/{input.TranslationMemoryId}/imports");
        request.Headers.Add("Authorization", Creds.First(p => p.KeyName == "Authorization").Value);
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(JsonConvert.SerializeObject(new
        {
            sourceLanguageCode = input.SourceLanguage,
            targetLanguageCode = input.TargetLanguage
        })), "properties");
        content.Add(new StreamContent(memoryStream), "file", input.File.Name);
        request.Content = content;
        var response = client.Send(request);
        response.EnsureSuccessStatusCode();
        var importOperationResult = response.Content.ReadFromJsonAsync<ImportTmxDto>().Result;
        restClient.PollImportTMXOperation(importOperationResult.Id, Creds);
    }
}