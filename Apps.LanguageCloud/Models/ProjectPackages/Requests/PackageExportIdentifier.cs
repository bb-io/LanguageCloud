using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectPackages.Requests;

public class PackageExportIdentifier
{
    [Display("Export ID")]
    public string ExportId { get; set; } = string.Empty;
}
