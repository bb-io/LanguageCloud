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
    [Action("List translation memories", Description = "List translation memories")]
    public ListTranslationMemoriesResponse ListTranslationMemories()
    {
        var request = new LanguageCloudRequest($"/translation-memory", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<TranslationMemoryDto>>>(request)!;
        response.Items.ForEach(p => p.GroupLanguageDirections());

        return new ListTranslationMemoriesResponse
        {
            Memories = response.Items
        };
    }

    [Action("Create translation memory", Description = "Create translation memory")]
    public TranslationMemoryDto CreateTranslationMemory([ActionParameter] CreateTranslationMemoryRequest input)
    {
        var request = new LanguageCloudRequest($"/translation-memory", Method.Post, Creds)
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
        var translationMemory = Client.Execute<TranslationMemoryDto>(request).Data!;
        translationMemory.GroupLanguageDirections();
        return translationMemory;
    }

    [Action("Get translation memory", Description = "Get translation memory")]
    public TranslationMemoryDto GetTranslationMemory([ActionParameter] GetTranslationMemoryRequest input)
    {
        var request = new LanguageCloudRequest($"/translation-memory/{input.TranslationMemoryId}", Method.Get, Creds);
        var response = Client.Get<TranslationMemoryDto>(request)!;
        response.GroupLanguageDirections();
        return response;
    }

    [Action("Import TMX file", Description = "Import TMX file")]
    public async Task ImportTmx([ActionParameter] ImportTmxRequest input)
    {
        var restClient = new LanguageCloudClient();

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
        restClient.PollImportTMXOperation(importOperationResult.Id, Creds);
    }
}