using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Files.Responses;

public class UploadFileResponse
{
    public string Name { get; set; }

    [Display("File ID")]
    public string Id { get; set; }

    public string Role { get; set; }
}