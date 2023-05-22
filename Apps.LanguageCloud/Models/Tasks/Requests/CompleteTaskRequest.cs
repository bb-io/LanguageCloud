using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Tasks.Requests
{
    public class CompleteTaskRequest
    {
        public string Id { get; set; }

        public string Outcome { get; set; }

        public string Comment { get; set; }
    }
}
