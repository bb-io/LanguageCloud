using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class TermBaseDataHandler : LanguageCloudInvocable, IAsyncDataSourceHandler
    {
        public TermBaseDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
            
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest($"/termbases", Method.Get);
            var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<TermbaseDto>>>(request);
            return response.Items
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
