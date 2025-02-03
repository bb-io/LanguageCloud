using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class LanguageDataHandler(InvocationContext invocationContext)
        : LanguageCloudInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/languages", Method.Get);
            request.AddQueryParameter("fields", "languageCode,englishName");
            var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<LanguageDto>>>(request);

            return response.Items
                .Where(x => context.SearchString == null ||
                            x.EnglishName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .Select(x => new DataSourceItem(x.LanguageCode, x.EnglishName));
        }
    }
}
