using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class FileProcessingConfigurationDataHandler : LanguageCloudInvocable, IAsyncDataSourceHandler
    {
        public FileProcessingConfigurationDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/file-processing-configurations", Method.Get);
            var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<FileProcessingConfigurationDto>>>(request);

            return response.Items
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
