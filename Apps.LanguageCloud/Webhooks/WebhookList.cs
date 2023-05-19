using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Webhooks.Handlers;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Text.Json;

namespace Apps.LanguageCloud.Webhooks
{
    [WebhookList]
    public class WebhookList 
    {
        #region ProjectWebhooks

        [Webhook("On project created", typeof(ProjectCreationHandler), Description = "On project created")]
        public async Task<WebhookResponse<ProjectDto>> ProjectCreation(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<ProjectDto>(webhookRequest.Body.ToString());
            if(data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectDto>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }

        #endregion

    }
}
