using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class AddTargetLanguageRequest
{
    [DataSource(typeof(ProjectDataHandler))]
    public string Project { get; set; }

    public IEnumerable<string> TargetLanguages { get; set; }
}