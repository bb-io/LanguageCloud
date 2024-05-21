using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class FolderDto
{
    [Display("Folder ID")]
    public string Id { get; set; }
    public string Name { get; set; }
}