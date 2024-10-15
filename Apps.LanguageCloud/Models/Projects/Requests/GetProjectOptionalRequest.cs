using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class GetProjectOptionalRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string? ProjectId { get; set; }
}