using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Tasks.Requests
{
    public class AssignTaskRequest
    {
        public string TaskId { get; set; }
        public string AssigneeId { get; set; }

        public string Type { get; set; }
    }
}
