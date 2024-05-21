using Blackbird.Applications.Sdk.Common;

namespace Apps.OpenAI.Models.Responses;

public class FileResponse
{
    [Display("File name")]
    public string FileName { get; set; }

    [Display("Last modified")]
    public string LastModified { get; set; }
}