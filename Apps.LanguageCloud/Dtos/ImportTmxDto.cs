using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class ImportTmxDto
{
    [Display("Translation memory ID")]
    public string Id { get; set; }

    public string Status { get; set; }


}