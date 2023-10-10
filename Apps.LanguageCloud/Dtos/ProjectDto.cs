using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class ProjectDto
{
    public string Id { get; set; }

    [Display("Short ID")]
    public string ShortId { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    [Display("Due by")]
    public string DueBy { get; set; }

    [Display("Created at")]
    public string CreatedAt { get; set; }

    public string Status { get; set; }

    [Display("Language directions")]
    [JsonProperty("languageDirections")]
    public List<LanguageDirection> LanguageDirections { get; set; }
}

public class LanguageDirection
{
    [JsonProperty("id")]
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
}

public class TargetLanguage
{
    [Display("Language code")]
    [JsonProperty("languageCode")]
    public string LanguageCode { get; set; }

    [Display("Is neutral")]
    [JsonProperty("isNeutral")]
    public bool IsNeutral { get; set; }
}