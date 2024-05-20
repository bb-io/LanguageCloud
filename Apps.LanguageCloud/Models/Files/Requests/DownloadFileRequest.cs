using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class DownloadFileRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }

    [Display("File ID")]
    public string FileId { get; set; }
}