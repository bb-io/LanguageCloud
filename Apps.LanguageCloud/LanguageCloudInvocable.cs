using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud
{
    public class LanguageCloudInvocable : BaseInvocable
    {
        protected AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

        protected LanguageCloudClient Client { get; }

        public LanguageCloudInvocable(InvocationContext invocationContext) : base(invocationContext)
        {
            Client = new();
        }
    }
}