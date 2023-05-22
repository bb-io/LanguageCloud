using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Webhooks.Payload
{
    public class ProjectGroupEvent
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public List<ProjectObj> Projects { get; set; }
    }
}
