using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Dtos
{
    public class TaskType
    {
        [Display("Task ID")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [Display("Api internal ID")]
        [JsonProperty("apiInternalId")]
        public string ApiInternalId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("automatic")]
        public bool Automatic { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("outcomes")]
        public List<Outcome> Outcomes { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public class Outcome
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }
    }

    public class Path
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [Display("Has parent")]
        [JsonProperty("hasParent")]
        public bool HasParent { get; set; }
    }

    public class Location
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [Display("Has parent")]
        [JsonProperty("hasParent")]
        public bool HasParent { get; set; }

        [JsonProperty("path")]
        public List<Path> Path { get; set; }
    }
}
