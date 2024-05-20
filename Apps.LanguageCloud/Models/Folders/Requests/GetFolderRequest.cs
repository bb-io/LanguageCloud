using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Folders.Requests;

public class GetFolderRequest
{
    [Display("Folder ID")]
    public string FolderId { get; set; }
}