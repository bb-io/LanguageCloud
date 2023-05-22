using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Webhooks.Payload
{
    public class TargetFileEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public ProjectObj Project { get; set; }
    }
}
