﻿using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using RestSharp;

namespace Apps.LanguageCloud.DataSourceHandlers;

public class FieldTemplateDataHandler : LanguageCloudInvocable, IAsyncDataSourceItemHandler
{
    public FieldTemplateDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new LanguageCloudRequest("/translation-memory/field-templates", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<FieldTemplateDto>>>(request);

        return response.Items
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}