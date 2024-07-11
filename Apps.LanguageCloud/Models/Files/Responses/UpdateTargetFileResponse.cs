

using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Files.Responses
{
    public class UpdateTargetFileResponse
    {
        [Display("File Import Status")]
        public string ImportStatus { get; set; }

        [Display("New Target File Version ID")]
        public string FileVersionId { get; set; }

    }
}
