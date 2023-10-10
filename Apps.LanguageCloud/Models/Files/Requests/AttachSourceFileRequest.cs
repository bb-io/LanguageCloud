
using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class AttachSourceFileRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }

    public string Name { get; set; }

    [Display("File ID")]
    public string FileId { get; set; }


    [Display("Language code")]
    [DataSource(typeof(LanguageDataHandler))]
    public string LanguageCode { get; set; }
}