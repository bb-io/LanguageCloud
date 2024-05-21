using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class LanguageDataHandler : LanguageCloudInvocable, IAsyncDataSourceHandler
    {
        private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

        public LanguageDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/languages", Method.Get, Creds);
            var response = Client.Get<ResponseWrapper<List<LanguageDto>>>(request);

            return response.Items
                .Where(x => context.SearchString == null ||
                            x.EnglishName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.LanguageCode, x => x.EnglishName);
        }
    }
}
