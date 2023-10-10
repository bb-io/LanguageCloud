using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class UploadFileRequest
{
    public string ProjectId { get; set; }

    public File File { get; set; }


    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguageCode { get; set; }
}