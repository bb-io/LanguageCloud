﻿using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using RestSharp;
namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class WorkflowDataHandler : LanguageCloudInvocable, IAsyncDataSourceHandler
    {
        private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

        public WorkflowDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var request = new LanguageCloudRequest("/workflows", Method.Get, Creds);
            var response = Client.Get<ResponseWrapper<List<WorkflowDto>>>(request);

            return response.Items
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
