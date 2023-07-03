using Apps.LanguageCloud.Webhooks.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Webhooks
{
    [WebhookList]
    public class WebhookList 
    {
        #region ProjectWebhooks

        [Webhook("On project created", Description = "On project created")]
        public async Task<WebhookResponse<ProjectEvent>> ProjectCreation(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectEvent>>(webhookRequest.Body.ToString());
            if(data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On project updated", Description = "On project updated")]
        public async Task<WebhookResponse<ProjectEvent>> ProjectUpdated(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On project deleted", Description = "On project deleted")]
        public async Task<WebhookResponse<ProjectEvent>> ProjectDeleted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        #endregion

        #region ProjectGroupWebhooks
        [Webhook("On task accepted", Description = "On task accepted")]
        public async Task<WebhookResponse<TaskEvent>> TaskAccepted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On task created", Description = "On task created")]
        public async Task<WebhookResponse<TaskEvent>> TaskCreated(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On task completed", Description = "On task completed")]
        public async Task<WebhookResponse<TaskEvent>> TaskCompleted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On task updated", Description = "On task updated")]
        public async Task<WebhookResponse<TaskEvent>> TaskUpdated(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On task deleted", Description = "On task deleted")]
        public async Task<WebhookResponse<TaskEvent>> TaskDeleted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }
        #endregion

        #region ProjectTemplateWebhooks

        [Webhook("On project template created", Description = "On project template created")]
        public async Task<WebhookResponse<ProjectTemplateEvent>> ProjectTemplateCreation(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectTemplateEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectTemplateEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On project template updated", Description = "On project template updated")]
        public async Task<WebhookResponse<ProjectTemplateEvent>> ProjectTemplateUpdated(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectTemplateEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectTemplateEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On project template deleted", Description = "On project template deleted")]
        public async Task<WebhookResponse<ProjectTemplateEvent>> ProjectTemplateDeleted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectTemplateEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectTemplateEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }
        #endregion

        #region SourceFileWebhooks

        [Webhook("On source file created", Description = "On source file created")]
        public async Task<WebhookResponse<SourceFileEvent>> SourceFileCreation(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<SourceFileEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<SourceFileEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On source file updated", Description = "On source file updated")]
        public async Task<WebhookResponse<SourceFileEvent>> SourceFileUpdated(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<SourceFileEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<SourceFileEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On source file deleted", Description = "On source file deleted")]
        public async Task<WebhookResponse<SourceFileEvent>> SourceFileDeleted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<SourceFileEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<SourceFileEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }
        #endregion

        #region TargetFileWebhooks

        [Webhook("On target file created", Description = "On target file created")]
        public async Task<WebhookResponse<TargetFileEvent>> TargetFileCreation(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TargetFileEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TargetFileEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On target file updated", Description = "On target file updated")]
        public async Task<WebhookResponse<TargetFileEvent>> TargetFileUpdated(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TargetFileEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TargetFileEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On target file deleted", Description = "On target file deleted")]
        public async Task<WebhookResponse<TargetFileEvent>> TargetFileDeleted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TargetFileEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TargetFileEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }
        #endregion

        #region ErrorTaskWebhooks

        [Webhook("On error task created", Description = "On error task created")]
        public async Task<WebhookResponse<ErrorTaskEvent>> ErrorTaskCreation(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ErrorTaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ErrorTaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On error task updated", Description = "On error task updated")]
        public async Task<WebhookResponse<ErrorTaskEvent>> ErrorTaskUpdated(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ErrorTaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ErrorTaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }

        [Webhook("On error task deleted", Description = "On error task deleted")]
        public async Task<WebhookResponse<ErrorTaskEvent>> ErrorTaskDeleted(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ErrorTaskEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ErrorTaskEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }
        #endregion

        [Webhook("On group project membership changed", Description = "On group project membership changed")]
        public async Task<WebhookResponse<ProjectGroupEvent>> MembershipChanged(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectGroupEvent>>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<ProjectGroupEvent>
            {
                HttpResponseMessage = null,
                Result = data.Data
            };
        }
    }
}
