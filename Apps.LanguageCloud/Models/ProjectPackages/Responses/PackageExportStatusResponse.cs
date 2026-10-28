using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectPackages.Responses;

public class PackageExportStatusResponse
{
    [Display("Export ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Status")]
    public string Status { get; set; } = string.Empty;

    [Display("Project ID")]
    public string? ProjectId { get; set; }

    [Display("Task IDs")]
    public IEnumerable<string>? TaskIds { get; set; }

    [Display("Expires at")]
    public DateTime? ExpiresAt { get; set; }

    [Display("Error")]
    public PackageOperationError? Error { get; set; }
}
