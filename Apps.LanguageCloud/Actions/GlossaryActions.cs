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
    public class GlossaryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
        : LanguageCloudInvocable(invocationContext)
    {
        [Action("Export glossary", Description = "Export glossary")]
        public async Task<ExportGlossaryResponse> ExportGlossary([ActionParameter] ExportGlossaryRequest input)
        {
            var exportRequest = new LanguageCloudRequest($"/termbases/{input.GlossaryId}/exports",
                Method.Post, Creds);
            exportRequest.AddJsonBody(new { });
            var exportOperation = Client.Execute<ExportTargetVersionDto>(exportRequest).Data;
            var pollingResult = Client.PollExportGlossariesOperation(exportOperation.Id, input.GlossaryId, Creds);
            var downloadRequest = new LanguageCloudRequest($"/termbases/{input.GlossaryId}/exports/{pollingResult.Id}/download",
                Method.Get, Creds);
            var fileData = Client.Get(downloadRequest).RawBytes;

            var requestGlossaryDetails = new LanguageCloudRequest($"/termbases/{input.GlossaryId}", Method.Get, Creds);
            var glossaryDetails = Client.Get<TermbaseDto>(requestGlossaryDetails);

            using var streamGlossaryData = new MemoryStream(fileData);
            using var resultStream = await streamGlossaryData.ConvertFromTBXV2ToV3(glossaryDetails.Name);
            var file = await fileManagementClient.UploadAsync(resultStream, MediaTypeNames.Application.Xml, $"{glossaryDetails.Name}.tbx");
            return new ExportGlossaryResponse() { File = file };
        }

        [Action("Import glossary", Description = "Import glossary")]
        public async Task ImportGlossary([ActionParameter] ImportGlossaryRequest input)
        {
            var fileStream = await fileManagementClient.DownloadAsync(input.File);
            var fileTBXV2Stream = await fileStream.ConvertFromTBXV3ToV2();

            var request = new LanguageCloudRequest($"/termbases/{input.GlossaryId}/imports", Method.Post, Creds);

            request.AddFile("file", fileTBXV2Stream.GetByteData().Result, input.File.Name);
            var importGlossaryRequest = Client.Execute<ImportTmxDto>(request).Data;

            Client.PollImportGlossariesOperation(importGlossaryRequest.Id, input.GlossaryId, Creds);
        }
    }
}
