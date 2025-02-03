using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Apps.LanguageCloud.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class ProjectTemplateDataHandler : LanguageCloudInvocable, IAsyncDataSourceItemHandler
    {
        public ProjectTemplateDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/project-templates", Method.Get);
            request.AddQueryParameter("top", 20);
            if (context.SearchString != null)
            {
                request.AddQueryParameter("name", context.SearchString);
            }
            var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<ProjectTemplateDto>>>(request);

            return response.Items.Select(x => new DataSourceItem(x.Id, x.Name));
        }
    }
}
