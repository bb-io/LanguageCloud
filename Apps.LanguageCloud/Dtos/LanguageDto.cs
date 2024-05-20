using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class LanguageDto
{
    [Display("Language Code")]
    public string LanguageCode { get; set; }

    [Display("English Name")]
    public string EnglishName { get; set; }
}