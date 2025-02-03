using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class ProjectDto
{
    [Display("Project ID")]
    public string Id { get; set; }

    [Display("Short ID")]
    [JsonProperty("shortId")]
    public string ShortId { get; set; }

    [Display("Project name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [Display("Due by")]
    [JsonProperty("dueBy")]
    public string DueBy { get; set; }

    [Display("Created at")]
    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [DefinitionIgnore]
    [JsonProperty("languageDirections")]
    public List<LanguageDirection> LanguageDirections { get; set; }
    
    [Display("Language directions")]
    public List<GroupedLanguageDirections> GroupedLanguageDirections { get => LanguageDirections
            .GroupBy(ld => ld.SourceLanguage)
            .Select(g => new GroupedLanguageDirections()
            {
                SourceLanguage = g.Key,
                TargetLanguages = g.Select(ld => ld.TargetLanguage).ToList()
            }).ToList();
        }
   
    [Display("Location")]
    [JsonProperty("location")]
    public folder Location { get; set; }
}

public class GroupedLanguageDirections
{
    [Display("Source language")]
    [JsonProperty("sourceLanguage")]
    public SourceLanguage SourceLanguage { get; set; }

    [Display("Target languages")]
    [JsonProperty("targetLanguages")]
    public List<TargetLanguage> TargetLanguages { get; set; }
}

public class LanguageDirection
{
    [JsonProperty("id"), Display("Language direction ID")]
    public string Id { get; set; }

    [Display("Source language")]
    [JsonProperty("sourceLanguage")]
    public SourceLanguage SourceLanguage { get; set; }

    [Display("Target language")]
    [JsonProperty("targetLanguage")]
    public TargetLanguage TargetLanguage { get; set; }
}

public class SourceLanguage
{
    [Display("Language code")]
    [JsonProperty("languageCode")]
    public string LanguageCode { get; set; }

    [Display("Is neutral")]
    [JsonProperty("isNeutral")]
    public bool IsNeutral { get; set; }

    public override int GetHashCode()
    {
        return LanguageCode.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is SourceLanguage language && language.LanguageCode == LanguageCode;
    }
}

public class TargetLanguage
{
    [Display("Language code")]
    [JsonProperty("languageCode")]
    public string LanguageCode { get; set; }

    [Display("Is neutral")]
    [JsonProperty("isNeutral")]
    public bool IsNeutral { get; set; }
    
    public override int GetHashCode()
    {
        return LanguageCode.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is SourceLanguage language && language.LanguageCode == LanguageCode;
    }
}

public class folder 
{
    [Display("Folder ID")]
    public string id { get; set; }

    [Display("Folder name")]
    public string name { get; set; }
}