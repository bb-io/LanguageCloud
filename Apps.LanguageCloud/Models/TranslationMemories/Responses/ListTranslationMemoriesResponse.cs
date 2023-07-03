using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.TranslationMemories.Responses
{
    public class ListTranslationMemoriesResponse
    {
        public IEnumerable<TranslationMemoryDto> Memories { get; set; }
    }
}
