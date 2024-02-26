using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class ProjectDto
{
    public string Id { get; set; }

    [Display("Short ID")]
    [JsonProperty("shortId")]
    public string ShortId { get; set; }

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

    [Display("Language directions")]
    [JsonProperty("languageDirections")]
    public List<LanguageDirection> LanguageDirections { get; set; }
   
    [Display("Folder Location")]
    [JsonProperty("location")]
    public folder Location { get; set; }
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

public class folder 
{
    public string id { get; set; }

    public string name { get; set; }

}