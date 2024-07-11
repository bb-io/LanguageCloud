using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class DownloadFileRequest
{
    [DataSource(typeof(ProjectDataHandler))]
    [Display("Project ID")]
    public string ProjectId { get; set; }

    [Display("File ID")]
    public string FileId { get; set; }

    [StaticDataSource(typeof(FileTypeDataHandler))]
    public string? Format { get; set; }
}