using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.TranslationMemories.Requests;
using Apps.LanguageCloud.Models.TranslationMemories.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Json;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class TranslationMemoryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : LanguageCloudInvocable(invocationContext)
{

    [Action("Create translation memory", Description = "Create translation memory")]
    public async Task<TranslationMemoryDto> CreateTranslationMemory([ActionParameter] CreateTranslationMemoryRequest input)
    {
        var request = new LanguageCloudRequest($"/translation-memory", Method.Post)
            .AddJsonBody(new
            {
                name = input.Name,
                languageDirections = new[]
                {
                    new
                    {
                        sourceLanguage = new { languageCode = input.SourceLanguage },
                        targetLanguage = new { languageCode = input.TargetLanguage }
                    }
                },
                languageProcessingRuleId = input.LanguageProcessingRuleId,
                fieldTemplateId = input.FieldTemplateId
            });
        var translationMemory = await Client.ExecuteWithErrorHandling<TranslationMemoryDto>(request);
        translationMemory.GroupLanguageDirections();
        return translationMemory;
    }

    [Action("Get translation memory", Description = "Get translation memory")]
    public async Task<TranslationMemoryDto> GetTranslationMemory([ActionParameter] GetTranslationMemoryRequest input)
    {
        var request = new LanguageCloudRequest($"/translation-memory/{input.TranslationMemoryId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<TranslationMemoryDto>(request);
        response.GroupLanguageDirections();
        return response;
    }

    [Action("Import TMX file", Description = "Import TMX file")]
    public async Task ImportTmx([ActionParameter] ImportTmxRequest input)
    {
        await using var memoryStream = await fileManagementClient.DownloadAsync(input.File);
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"https://lc-api.sdl.com/public-api/v1/translation-memory/{input.TranslationMemoryId}/imports");
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
        await Client.PollImportTMXOperation(importOperationResult.Id);
    }
}