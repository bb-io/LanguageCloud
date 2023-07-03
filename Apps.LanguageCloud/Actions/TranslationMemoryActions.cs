using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.TranslationMemories.Requests;
using Apps.LanguageCloud.Models.TranslationMemories.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Json;

namespace Apps.LanguageCloud.Actions
{
    [ActionList]
    public class TranslationMemoryActions
    {
        [Action("List translation memories", Description = "List translation memories")]
        public ListTranslationMemoriesResponse ListTranslationMemories(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/translation-memory", Method.Get, authenticationCredentialsProviders);
            var response = client.Get<ResponseWrapper<List<TranslationMemoryDto>>>(request);
            return new ListTranslationMemoriesResponse()
            {
                Memories = response.Items
            };
        }

        [Action("Create translation memory", Description = "Create translation memory")]
        public TranslationMemoryDto CreateTranslationMemory(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] CreateTranslationMemoryRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/translation-memory", Method.Post, authenticationCredentialsProviders);
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
            return client.Execute<TranslationMemoryDto>(request).Data;
        }

        [Action("Get translation memory", Description = "Get translation memory")]
        public TranslationMemoryDto GetTranslationMemory(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetTranslationMemoryRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/translation-memory/{input.TranslationMemoryId}", Method.Get, authenticationCredentialsProviders);
            var response = client.Get<TranslationMemoryDto>(request);
            return response;
        }

        [Action("Import TMX file", Description = "Import TMX file")]
        public void ImportTmx(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] ImportTmxRequest input)
        {
            var restClient = new LanguageCloudClient(authenticationCredentialsProviders);
            using var memoryStream = new MemoryStream(input.File);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://lc-api.sdl.com/public-api/v1/translation-memory/{input.TranslationMemoryId}/imports");
            request.Headers.Add("Authorization", authenticationCredentialsProviders.First(p => p.KeyName == "Authorization").Value);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(JsonConvert.SerializeObject(new
            {
                sourceLanguageCode = input.SourceLanguage,
                targetLanguageCode = input.TargetLanguage
            })), "properties");
            content.Add(new StreamContent(memoryStream), "file", input.Filename);
            request.Content = content;
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            var importOperationResult = response.Content.ReadFromJsonAsync<ImportTmxDto>().Result;
            restClient.PollImportTMXOperation(importOperationResult.Id, authenticationCredentialsProviders);
        }
    }
}
