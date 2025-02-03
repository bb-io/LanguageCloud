using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class LocationDataHandler : LanguageCloudInvocable, IAsyncDataSourceItemHandler
    {
        public LocationDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/folders", Method.Get);
            if (context.SearchString != null)
                request.AddQueryParameter("name", context.SearchString);
            request.AddQueryParameter("top", 20);
            var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<FolderDto>>>(request);


            return response.Items.Select(x => new DataSourceItem(x.Id, x.Name));
        }
    }
}
