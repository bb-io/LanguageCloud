using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.LanguageCloud.Models.ProjectPackages.Requests;

public class ImportPackageRequest
{
    [Display("Package", Description = "Completed .sdlppx or .sdlrpx package.")]
    public FileReference File { get; set; } = new();
}
