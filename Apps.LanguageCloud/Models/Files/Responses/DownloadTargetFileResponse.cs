using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;


namespace Apps.LanguageCloud.Models.Files.Responses
{
    public class DownloadTargetFileResponse
    {
        [Display("File")]
        public File File { get; set; }
    }
}
