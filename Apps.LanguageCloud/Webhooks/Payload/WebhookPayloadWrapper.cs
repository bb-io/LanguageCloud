using Newtonsoft.Json;

namespace Apps.LanguageCloud.Webhooks.Payload;

public class WebhookPayloadWrapper<T>
{
    [JsonProperty("accountId")]
    public string AccountId { get; set; } = string.Empty;
    
    public T Data { get; set; }
}