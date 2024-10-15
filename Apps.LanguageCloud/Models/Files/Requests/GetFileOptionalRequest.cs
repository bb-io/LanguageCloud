using Apps.LanguageCloud.Models.Projects.Requests;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class GetFileOptionalRequest : GetProjectOptionalRequest
{
    [Display("File ID")]
    public string? FileId { get; set; }
}