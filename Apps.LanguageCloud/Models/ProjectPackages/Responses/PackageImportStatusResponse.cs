using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectPackages.Responses;

public class PackageImportStatusResponse
{
    [Display("Import ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Status")]
    public string Status { get; set; } = string.Empty;

    [Display("Error")]
    public PackageOperationError? Error { get; set; }
}
