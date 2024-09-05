﻿using Newtonsoft.Json;

namespace Apps.LanguageCloud.Models.Accounts;

public class AccountResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}