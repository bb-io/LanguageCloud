using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class FileInfoDto
{
    [Display("File name")]
    public string Name { get; set; }

    [Display("File ID")]
    public string ID { get; set; }

   // public string Role { get; set; }

    [JsonProperty("latestVersion"), Display("Latest version")]
    public VersionDto LatestVersion { get; set; }
}

public class VersionDto
{
    [JsonProperty("id"), Display("Version ID")]
    public string Id { get; set; }

    [JsonProperty("type"), Display("Version type")]
    public string Type { get; set; }
    
    [JsonProperty("status"), Display("Version")]
    public int version { get; set; }
}