using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class TranslationEngineDataHandler : LanguageCloudInvocable, IAsyncDataSourceHandler
    {
        private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

        public TranslationEngineDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/translation-engines", Method.Get, Creds);
            var response = Client.Get<ResponseWrapper<List<TranslationEngineDto>>>(request);

            return response.Items
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
