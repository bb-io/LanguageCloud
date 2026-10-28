using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectPackages.Responses;

public class PackageOperationError
{
    [Display("Error code")]
    public string? ErrorCode { get; set; }

    [Display("Error message")]
    public string? Message { get; set; }

    [Display("Error details")]
    public IEnumerable<PackageOperationErrorDetail>? Details { get; set; }
}

public class PackageOperationErrorDetail
{
    [Display("Name")]
    public string? Name { get; set; }
}
