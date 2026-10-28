using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.ProjectPackages.Requests;

public class ExportPackageRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; } = string.Empty;

    [Display("Task IDs", Description = "IDs of the tasks to export. If omitted, active human target-file tasks are selected automatically.")]
    public IEnumerable<string>? TaskIds { get; set; }

    [Display("Online package", Description = "Whether the package should use online termbases.")]
    public bool? OnlinePackage { get; set; }

    [Display("Include translation memories")]
    public bool? IncludeTm { get; set; }

    [Display("Include reference files")]
    public bool? IncludeReferenceFiles { get; set; }

    [Display("Split by language")]
    public bool? SplitByLanguage { get; set; }

    [Display("Files per package", Description = "Number of files per package. Applicable only when packages are not split by language.")]
    public int? FilesPerPackage { get; set; }
}
