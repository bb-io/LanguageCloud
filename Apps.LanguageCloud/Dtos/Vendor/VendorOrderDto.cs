using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos.Vendor;

public class VendorOrderDto
{
    [Display("Vendor order ID"), JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [Display("Vendor quote"), JsonProperty("quote")]
    public VendorOrderQuoteDto Quote { get; set; }
}
