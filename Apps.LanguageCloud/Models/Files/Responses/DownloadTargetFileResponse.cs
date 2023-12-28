using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;



namespace Apps.LanguageCloud.Models.Files.Responses;

public class DownloadTargetFileResponse
{
    [Display("File")]
    public FileReference File { get; set; }
}