using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Folders.Responses;

public class ListAllFoldersResponse
{
    public IEnumerable<FolderDto> Folders { get; set; }
}