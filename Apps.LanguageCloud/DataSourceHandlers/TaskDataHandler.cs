using Apps.LanguageCloud.Actions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class TaskDataHandler : BaseInvocable, IAsyncDataSourceHandler
    {
        private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

        public TaskDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var tasks = new TaskActions().ListAllTasks(Creds);
            return tasks.Tasks
                .Where(x => context.SearchString == null ||
                            x.TaskType.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase) ||
                            x.Project.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Id, x => $"{x.TaskType.Name} - {x.Project.Name}");
        }
    }
}
