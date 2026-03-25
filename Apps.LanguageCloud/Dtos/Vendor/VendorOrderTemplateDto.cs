using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos.Vendor;

public class VendorOrderTemplateDto
{
    [Display("Vendor order template ID"), JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
}
