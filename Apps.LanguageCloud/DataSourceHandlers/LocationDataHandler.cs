using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using Apps.LanguageCloud.Actions;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Apps.LanguageCloud.DataSourceHandlers
{
    public class LocationDataHandler : BaseInvocable, IAsyncDataSourceHandler
    {
        private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

        public LocationDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var client = new LanguageCloudClient(Creds);
            var folders = new FolderActions().ListAllFolders(Creds);
            

            return folders.Folders
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Id, x => x.Name);
        }
    }
}
