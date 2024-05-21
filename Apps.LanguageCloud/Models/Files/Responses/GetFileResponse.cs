using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Files.Responses;

public class GetFileResponse
{
    [Display("File content")]
    public string FileContent { get; set; }
}