using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Dtos
{
    public class LanguageProcessingRuleDto
    {
        [Display("Language Processing Rule ID")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [Display("Language Processing Rule Name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
