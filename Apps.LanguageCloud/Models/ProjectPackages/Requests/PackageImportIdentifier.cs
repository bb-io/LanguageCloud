using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectPackages.Requests;

public class PackageImportIdentifier
{
    [Display("Import ID")]
    public string ImportId { get; set; } = string.Empty;
}
