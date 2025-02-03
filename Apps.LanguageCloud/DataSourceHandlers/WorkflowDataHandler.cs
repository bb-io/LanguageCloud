using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using RestSharp;
namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class WorkflowDataHandler : LanguageCloudInvocable, IAsyncDataSourceItemHandler
    {
        public WorkflowDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/workflows", Method.Get);
            var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<WorkflowDto>>>(request);

            return response.Items
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Select(x => new DataSourceItem(x.Id, x.Name));
        }
    }
}
