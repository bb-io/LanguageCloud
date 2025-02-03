using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers;

public class TranslationMemoryDataSource(InvocationContext invocationContext)
    : LanguageCloudInvocable(invocationContext), IAsyncDataSourceItemHandler
{

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new LanguageCloudRequest($"/translation-memory", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<TranslationMemoryDto>>>(request);

        return response.Items
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}