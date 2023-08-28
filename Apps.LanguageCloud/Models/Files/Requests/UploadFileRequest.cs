using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.LanguageCloud.Models.Files.Requests
{
    public class UploadFileRequest
    {
        public string ProjectId { get; set; }

        public File File { get; set; }

        public string SourceLanguageCode { get; set; }
    }
}
