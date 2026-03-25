using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos.Vendor;

public class VendorOrderQuoteDto
{
    [Display("Project quote total amount"), JsonProperty("totalAmount")]
    public double? TotalAmount { get; set; }

    [Display("Project quote currency code"), JsonProperty("currencyCode")]
    public string CurrencyCode { get; set; } = string.Empty;

    [Display("Project quote notes"), JsonProperty("notes")]
    public string? Notes { get; set; }
}
