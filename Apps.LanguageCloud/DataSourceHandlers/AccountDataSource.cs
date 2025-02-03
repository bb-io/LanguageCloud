using Apps.LanguageCloud.Models.Accounts;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers;

public class AccountDataSource(InvocationContext invocationContext)
    : LanguageCloudInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new LanguageCloudRequest("/accounts", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<AccountResponse>>>(request);
        
        return response.Items
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}