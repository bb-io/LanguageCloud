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

    }
}
