using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class LanguageDto
{
    [Display("Language code")]
    public string LanguageCode { get; set; }

    [Display("English name")]
    public string EnglishName { get; set; }
}