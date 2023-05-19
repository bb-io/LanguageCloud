using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Webhooks.Handlers
{
    public class ProjectCreationHandler : BaseWebhookHandler
    {

        const string SubscriptionEvent = "PROJECT_CREATED";

        public ProjectCreationHandler() : base(SubscriptionEvent) { }
    }
}
