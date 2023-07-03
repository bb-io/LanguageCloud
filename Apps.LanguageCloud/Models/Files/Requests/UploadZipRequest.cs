namespace Apps.LanguageCloud.Models.Files.Requests
{
    public class UploadZipRequest
    {
        public byte[] File { get; set; }

        public string FileName { get; set; }
    }
}
