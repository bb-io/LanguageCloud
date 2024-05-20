using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Projects.Responses;

public class GetProjectResponse
{
    [Display("Project Name")]
    public string Name { get; set; }

    [Display("Project ID")]
    public string Id { get; set; }

    [Display("Date Created")]
    public string DateCreated { get; set; }
}