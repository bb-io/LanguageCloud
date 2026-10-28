using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectPackages.Responses;

public class ImportPackageResponse
{
    [Display("Import ID")]
    public string ImportId { get; set; } = string.Empty;
}
