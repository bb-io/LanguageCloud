using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;


namespace Apps.LanguageCloud.Models.Files.Requests;

public class UploadFileRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; }

    public FileReference File { get; set; }


    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguageCode { get; set; }

    [StaticDataSource(typeof(FileRoleDataHandler))]
    public string? Role { get; set; }

    [Display("File Type")]
    [StaticDataSource(typeof(FileTypeDataHandler))]
    public string? FileType { get; set; }
}