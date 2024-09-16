using Apps.LanguageCloud.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.LanguageCloud.DataSourceHandlers;

public class TranslationMemoryDataSource(InvocationContext invocationContext)
    : LanguageCloudInvocable(invocationContext), IAsyncDataSourceHandler
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    public Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var actions = new TranslationMemoryActions(InvocationContext, null!);
        var translationMemories = actions.ListTranslationMemories().Memories.ToList();

        return Task.FromResult(translationMemories
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(
                memory => memory.Id.ToString(),
                memory => memory.Name));
    }
}