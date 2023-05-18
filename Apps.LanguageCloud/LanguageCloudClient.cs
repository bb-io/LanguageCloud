using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud
{
    public class LanguageCloudClient : RestClient
    {
        public LanguageCloudClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : 
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
    }
}
