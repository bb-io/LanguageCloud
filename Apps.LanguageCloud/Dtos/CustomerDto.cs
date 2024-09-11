using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class CustomerDto
{
    [Display("Customer ID")]
    public string Id { get; set; }

    [Display("Customer name")]
    public string Name { get; set; }
}