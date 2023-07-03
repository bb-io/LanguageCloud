using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Files.Responses
{
    public class ListAllFilesResponse
    {
        public IEnumerable<FileInfoDto> Files { get; set; }
    }
}
