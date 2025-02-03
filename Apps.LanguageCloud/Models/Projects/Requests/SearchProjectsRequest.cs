using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Projects.Requests;
public class SearchProjectsRequest
{
    [Display("Created from")]
    public DateTime? CreatedFrom { get; set; }

    [Display("Created to")]
    public DateTime? CreatedTo { get; set; }

    [Display("Due from")]
    public DateTime? DueFrom { get; set; }

    [Display("Due to")]
    public DateTime? DueTo { get; set; }

    [Display("Exclude online projects")]
    public bool? ExcludeOnline { get; set; }

    [Display("Location")]
    [DataSource(typeof(LocationDataHandler))]
    public string? Location { get; set; }

    [Display("Project name")]
    public string? ProjectName { get; set; }

    [Display("Project template ID")]
    [DataSource(typeof(ProjectTemplateDataHandler))]
    public string? ProjectTemplateId { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? TargetLanguage { get; set; }

    [Display("Status")]
    [StaticDataSource(typeof(ProjectStatusDataHandler))]
    public string? Status { get; set; }
}
