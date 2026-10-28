using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.ProjectPackages.Responses;

public class ExportPackageResponse
{
    [Display("Export IDs")]
    public List<string> ExportIds { get; set; } = [];
}
