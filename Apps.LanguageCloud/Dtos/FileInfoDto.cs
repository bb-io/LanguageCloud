using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class FileInfoDto
{
    public string Name { get; set; }

    public string Id { get; set; }

   // public string Role { get; set; }

    [JsonProperty("latestVersion")]
    public VersionDto LatestVersion { get; set; }
}

public class VersionDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
    
    public int version { get; set; }
}