using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos;

public class TranslationMemoryDto
{
    [Display("Translation memory ID")]
    public string Id { get; set; }

    [Display("Translation memory name")]
    public string Name { get; set; }

    [DefinitionIgnore]
    public List<LanguageDirectionItem> LanguageDirections { get; set; }
    
    [Display("Language directions")]
    public List<GroupedLanguageDirectionItems> GroupedLanguageDirections { get; set; }
    
    public void GroupLanguageDirections()
    {
        GroupedLanguageDirections = LanguageDirections
            .GroupBy(p => p.LanguageDirection.SourceLanguage.LanguageCode)
            .Select(p => new GroupedLanguageDirectionItems
            {
                SourceLanguage = p.First().LanguageDirection.SourceLanguage,
                TargetLanguages = p.Select(q => q.LanguageDirection.TargetLanguage).ToList()
            }).ToList();
    }
}

public class GroupedLanguageDirectionItems
{
    [Display("Source language")]
    public LanguageObj SourceLanguage { get; set; } = new();
    
    [Display("Target languages")]
    public List<LanguageObj> TargetLanguages { get; set; } = new();
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
    
    public override int GetHashCode()
    {
        return LanguageCode.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is SourceLanguage language && language.LanguageCode == LanguageCode;
    }
}