using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class TranslationMemoryDto
{
    [Display("Translation memory ID")]
    public string Id { get; set; }

    [Display("Translation memory name")]
    public string Name { get; set; }

    [Display("Language directions")]
    public IEnumerable<LanguageDirectionItem> LanguageDirections { get; set; }
}

public class LanguageDirectionItem
{
    [Display("Language direction")]
    public LanguageDirectionObj LanguageDirection { get; set; }
}

public class LanguageDirectionObj
{
    [Display("Source language")]
    public LanguageObj SourceLanguage { get; set; }
    
    [Display("Target language")]
    public LanguageObj TargetLanguage { get; set; }
}

public class LanguageObj
{
    [Display("Language code")]
    public string LanguageCode { get; set; }
    
    [Display("Language name")]
    public string EnglishName { get; set; }
}