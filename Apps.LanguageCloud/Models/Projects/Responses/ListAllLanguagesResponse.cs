using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Projects.Responses
{
    public class ListAllLanguagesResponse
    {
        public IEnumerable<LanguageDto> Languages { get; set; }
    }
}
