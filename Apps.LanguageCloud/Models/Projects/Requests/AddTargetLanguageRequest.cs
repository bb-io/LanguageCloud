using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class AddTargetLanguageRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string Project { get; set; }

    [Display("Target Languages")]
    public IEnumerable<string> TargetLanguages { get; set; }
}