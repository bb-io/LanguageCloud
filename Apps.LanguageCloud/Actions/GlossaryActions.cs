using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Files.Responses;
using Apps.LanguageCloud.Models.Glossaries.Requests;
using Apps.LanguageCloud.Models.Glossaries.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

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

        //public async Task<ExportGlossaryResponse> ExportGlossary([ActionParameter] ExportGlossaryRequest input)
        //{
        //    var client = new PhraseTmsClient(InvocationContext.AuthenticationCredentialsProviders);

        //    var endpointGlossaryData = $"/api2/v1/termBases/{input.GlossaryUId}/export";
        //    var requestGlossaryData = new PhraseTmsRequest(endpointGlossaryData, Method.Get, InvocationContext.AuthenticationCredentialsProviders);
        //    var responseGlossaryData = await client.ExecuteAsync(requestGlossaryData);

        //    var endpointGlossaryDetails = $"/api2/v1/termBases/{input.GlossaryUId}";
        //    var requestGlossaryDetails = new PhraseTmsRequest(endpointGlossaryDetails, Method.Get, InvocationContext.AuthenticationCredentialsProviders);
        //    var responseGlossaryDetails = await client.ExecuteWithHandling<GlossaryDto>(requestGlossaryDetails);

        //    using var streamGlossaryData = new MemoryStream(responseGlossaryData.RawBytes);

        //    using var resultStream = await streamGlossaryData.ConvertFromTBXV2ToV3(responseGlossaryDetails.Name);
        //    return new ExportGlossaryResponse() { File = await _fileManagementClient.UploadAsync(resultStream, MediaTypeNames.Application.Xml, $"{responseGlossaryDetails.Name}.tbx") };
        //}

        [Action("Import glossary", Description = "Import glossary")]
        public async Task ImportGlossary([ActionParameter] ImportGlossaryRequest input)
        {
            var fileStream = await _fileManagementClient.DownloadAsync(input.File);
            var fileTBXV2Stream = await fileStream.ConvertFromTBXV3ToV2();

            var client = new LanguageCloudClient(InvocationContext.AuthenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/termbases/{input.GlossaryId}/imports", Method.Post, InvocationContext.AuthenticationCredentialsProviders);

            request.AddFile("file", fileTBXV2Stream.GetByteData().Result, input.File.Name);
            var importGlossaryRequest = client.Execute<ImportTmxDto>(request).Data;

            client.PollImportGlossariesOperation(importGlossaryRequest.Id, input.GlossaryId, InvocationContext.AuthenticationCredentialsProviders);
        }

        [Action("Test glossary", Description = "Test glossary")]
        public async Task<ExportGlossaryResponse> TestGlossary([ActionParameter] ImportGlossaryRequest input)
        {
            var fileStream = await _fileManagementClient.DownloadAsync(input.File);
            var fileTBXV2Stream = await fileStream.ConvertFromTBXV3ToV2();

            var file = await _fileManagementClient.UploadAsync(fileTBXV2Stream, MediaTypeNames.Application.Xml, $"Test11.tbx");
            return new ExportGlossaryResponse() { File = file };
        }

    }
}
