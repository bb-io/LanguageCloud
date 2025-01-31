using Apps.LanguageCloud.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.LanguageCloud.DataSourceHandlers;

public class TranslationMemoryDataSource(InvocationContext invocationContext)
    : LanguageCloudInvocable(invocationContext), IAsyncDataSourceHandler
{

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var actions = new TranslationMemoryActions(InvocationContext, null!);
        var translationMemories = (await actions.ListTranslationMemories()).Memories.ToList();

        return translationMemories
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(
                memory => memory.Id.ToString(),
                memory => memory.Name);
    }
}