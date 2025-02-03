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
    public class ProjectDataHandler : LanguageCloudInvocable, IAsyncDataSourceItemHandler
    {
        public ProjectDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/projects", Method.Get);
            request.AddQueryParameter("fields", "id,name");
            request.AddQueryParameter("top", 20);

            if (context.SearchString != null)
            {
                request.AddQueryParameter("projectName", context.SearchString);
            }

            var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<ProjectDto>>>(request);

            return response.Items.Select(x => new DataSourceItem(x.Id, x.Name));
        }
    }
}
