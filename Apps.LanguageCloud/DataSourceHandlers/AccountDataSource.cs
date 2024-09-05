using Apps.LanguageCloud.Models.Accounts;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers;

public class AccountDataSource(InvocationContext invocationContext)
    : LanguageCloudInvocable(invocationContext), IAsyncDataSourceHandler
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    public Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new LanguageCloudRequest("/accounts", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<AccountResponse>>>(request);
        
        return Task.FromResult(response.Items
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id, x => x.Name));
    }
}