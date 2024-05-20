using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class ListSourceFilesRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
}