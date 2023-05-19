using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.LanguageCloud.Webhooks.Handlers
{

    public class BaseWebhookHandler : IWebhookEventHandler
    {

        private string SubscriptionEvent;

        public BaseWebhookHandler(string subEvent)
        {
            SubscriptionEvent = subEvent;
        }
        // No api for subscription
        public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            //var authHeader = authenticationCredentialsProvider.First(p => p.KeyName == "Authorization").Value;
            //var client = new LanguageCloudClient(authenticationCredentialsProvider);
            //var request = new LanguageCloudRequest($"/api2/v2/webhooks", Method.Post, authenticationCredentialsProvider);
            //request.AddJsonBody(new
            //{
            //    events = new[] { SubscriptionEvent },
            //    url = values["payloadUrl"],
            //    name = SubscriptionEvent
            //});
            //await client.ExecuteAsync(request);
        }

        public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            //var authHeader = authenticationCredentialsProvider.First(p => p.KeyName == "Authorization").Value;
            //var client = new LanguageCloudClient(authenticationCredentialsProvider);
            //var getRequest = new LanguageCloudRequest($"/api2/v2/webhooks?name={SubscriptionEvent}", Method.Get, authenticationCredentialsProvider);
            //var webhooks = await client.GetAsync<ResponseWrapper<List<WebhookDto>>>(getRequest);
            //var webhookUId = webhooks.Content.First().UId;

            //var deleteRequest = new LanguageCloudRequest($"/api2/v2/webhooks/{webhookUId}", Method.Delete, authenticationCredentialsProvider);
            //await client.ExecuteAsync(deleteRequest);
        }
    }
}
