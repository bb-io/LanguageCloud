﻿using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.LanguageCloud.Connections;

public class OAuth2ConnectionDefinition : IConnectionDefinition
{
    private const string ApiKeyName = "apiToken";

    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>()
    {
        new()
        {
            Name = "ApiToken",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = new List<ConnectionProperty>()
            {
                new("apiKey") { DisplayName = "API key", Sensitive = true },
                new("tenantId") { DisplayName = "Tenant ID" },
            }
        },
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var token = values.First(v => v.Key == "apiKey");
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.Header,
            "Authorization",
            $"LC apiKey={token.Value}"
        );
        var url = values.First(v => v.Key == "tenantId");
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            "tenantId",
            url.Value
        );
    }
}