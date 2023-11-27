using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.LanguageCloud.Models.Files.Requests;

public class UploadFileRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; }

    public File File { get; set; }


    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguageCode { get; set; }

    [DataSource(typeof(FileRoleDataHandler))]
    public string? Role { get; set; }

    [Display("Type")]
    [DataSource(typeof(FileTypeDataHandler))]
    public string? FileType { get; set; }
}