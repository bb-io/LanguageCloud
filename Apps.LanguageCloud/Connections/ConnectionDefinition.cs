using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.LanguageCloud.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    private const string ApiKeyName = "apiToken";

    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>()
    {
        new()
        {
            Name = "ApiToken",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>()
            {
                new(CredsNames.ClientId) { DisplayName = "Client ID" },
                new(CredsNames.ClientSecret) { DisplayName = "Client secret", Sensitive = true },
                new("tenantId") { DisplayName = "Tenant ID" },
            }
        },
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        yield return new AuthenticationCredentialsProvider(
            CredsNames.ClientId,
            values[CredsNames.ClientId]
        );

        yield return new AuthenticationCredentialsProvider(
            CredsNames.ClientSecret,
            values[CredsNames.ClientSecret]
        );

        var url = values.First(v => v.Key == "tenantId");
        yield return new AuthenticationCredentialsProvider(
            "tenantId",
            url.Value
        );
    }
}