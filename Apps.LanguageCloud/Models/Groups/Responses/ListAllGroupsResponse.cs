using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Groups.Responses;

public class ListAllGroupsResponse
{
    public IEnumerable<GroupDto> Groups { get; set; }
}