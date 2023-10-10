using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class CreateProjectRequest
{
    public string Name { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; }

    [Display("Target languages")]
    public List<string> TargetLanguages { get; set; }
}