using Apps.LanguageCloud.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Apps.LanguageCloud;

public class LanguageCloudRequest : RestRequest
{
    public LanguageCloudRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> creds) :
        base(endpoint, method)
    {
        var authCreds = GetAuthCreds(creds);
        this.AddHeader("Authorization", $"Bearer {authCreds.AccessToken}");
        this.AddHeader("X-LC-Tenant", creds.First(p => p.KeyName == "tenantId").Value);
        this.AddHeader("accept", "*/*");
    }

    private CredsResponse GetAuthCreds(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new RestRequest("https://sdl-prod.eu.auth0.com/oauth/token", Method.Post)
            .AddJsonBody(new
            {
                client_id = creds.Get(CredsNames.ClientId).Value,
                client_secret = creds.Get(CredsNames.ClientSecret).Value,
                grant_type = "client_credentials",
                audience = "https://api.sdl.com"
            });

        var response = new RestClient().Execute(request);

        return JsonConvert.DeserializeObject<CredsResponse>(response.Content, new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        });
    }
}