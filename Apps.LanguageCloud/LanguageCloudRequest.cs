using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.LanguageCloud
{
    public class LanguageCloudRequest : RestRequest
    {
        public LanguageCloudRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
        {
            this.AddHeader("Authorization", authenticationCredentialsProviders.First(p => p.KeyName == "Authorization").Value);
            this.AddHeader("X-LC-Tenant", authenticationCredentialsProviders.First(p => p.KeyName == "tenantId").Value);
            this.AddHeader("accept", "*/*");
        }
    }
}
