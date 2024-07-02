using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class ListSourceFilesRequest
{
    [DataSource(typeof(ProjectDataHandler))]
    [Display("Project ID")]
    public string ProjectId { get; set; }
}