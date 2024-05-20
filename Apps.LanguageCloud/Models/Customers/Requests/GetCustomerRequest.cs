using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Customers.Requests;

public class GetCustomerRequest
{
    [Display("Customer ID")]
    public string Id { get; set; }
}