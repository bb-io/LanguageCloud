using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Glossaries.Requests;
using Apps.LanguageCloud.Models.Glossaries.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Net.Mime;

namespace Apps.LanguageCloud.Actions
{
    [ActionList]
    public class GlossaryActions : BaseInvocable
    {
        private readonly IFileManagementClient _fileManagementClient;

        public GlossaryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
        {
            _fileManagementClient = fileManagementClient;
        }

        [Action("Export glossary", Description = "Export glossary")]
        public async Task<ExportGlossaryResponse> ExportGlossary([ActionParameter] ExportGlossaryRequest input)
        {
            var client = new LanguageCloudClient();

            var exportRequest = new LanguageCloudRequest($"/termbases/{input.GlossaryId}/exports",
                Method.Post, InvocationContext.AuthenticationCredentialsProviders);
            exportRequest.AddJsonBody(new { });
            var exportOperation = client.Execute<ExportTargetVersionDto>(exportRequest).Data;
            var pollingResult = client.PollExportGlossariesOperation(exportOperation.Id, input.GlossaryId, InvocationContext.AuthenticationCredentialsProviders);
            var downloadRequest = new LanguageCloudRequest($"/termbases/{input.GlossaryId}/exports/{pollingResult.Id}/download",
                Method.Get, InvocationContext.AuthenticationCredentialsProviders);
            var fileData = client.Get(downloadRequest).RawBytes;

            var requestGlossaryDetails = new LanguageCloudRequest($"/termbases/{input.GlossaryId}", Method.Get, InvocationContext.AuthenticationCredentialsProviders);
            var glossaryDetails = client.Get<TermbaseDto>(requestGlossaryDetails);

            using var streamGlossaryData = new MemoryStream(fileData);
            using var resultStream = await streamGlossaryData.ConvertFromTBXV2ToV3(glossaryDetails.Name);
            var file = await _fileManagementClient.UploadAsync(resultStream, MediaTypeNames.Application.Xml, $"{glossaryDetails.Name}.tbx");
            return new ExportGlossaryResponse() { File = file };
        }

        [Action("Import glossary", Description = "Import glossary")]
        public async Task ImportGlossary([ActionParameter] ImportGlossaryRequest input)
        {
            var fileStream = await _fileManagementClient.DownloadAsync(input.File);
            var fileTBXV2Stream = await fileStream.ConvertFromTBXV3ToV2();

            var client = new LanguageCloudClient();
            var request = new LanguageCloudRequest($"/termbases/{input.GlossaryId}/imports", Method.Post, InvocationContext.AuthenticationCredentialsProviders);

            request.AddFile("file", fileTBXV2Stream.GetByteData().Result, input.File.Name);
            var importGlossaryRequest = client.Execute<ImportTmxDto>(request).Data;

            client.PollImportGlossariesOperation(importGlossaryRequest.Id, input.GlossaryId, InvocationContext.AuthenticationCredentialsProviders);
        }
    }
}
