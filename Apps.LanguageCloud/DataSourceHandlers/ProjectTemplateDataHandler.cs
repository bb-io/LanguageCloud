using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Apps.LanguageCloud.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class ProjectTemplateDataHandler : LanguageCloudInvocable, IAsyncDataSourceHandler
    {
        public ProjectTemplateDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var projects = await new ProjectTemplateActions(InvocationContext).ListAllProjectTemplates();
            return projects.ProjectTemplates
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
