using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Webhooks.Payload
{
    public class WebhookPayloadWrapper<T>
    {
        public T Data { get; set; }
    }
}
