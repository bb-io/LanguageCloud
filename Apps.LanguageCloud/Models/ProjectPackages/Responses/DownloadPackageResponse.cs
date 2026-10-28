using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.LanguageCloud.Models.ProjectPackages.Responses;

public class DownloadPackageResponse
{
    [Display("Package")]
    public FileReference File { get; set; } = new();
}
