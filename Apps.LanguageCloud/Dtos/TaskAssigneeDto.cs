using Apps.LanguageCloud.Dtos.Vendor;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class TaskAssigneeDto
{
    [Display("Type"), JsonProperty("type")]
    public string Type { get; set; }

    [Display("User"), JsonProperty("user")]
    public UserDto User { get; set; }

    [Display("Group"), JsonProperty("group")]
    public GroupDto Group { get; set; }

    [Display("Vendor order template"), JsonProperty("vendorOrderTemplate")]
    public VendorOrderTemplateDto VendorOrderTemplate { get; set; }
}
