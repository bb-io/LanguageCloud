using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Projects.Requests
{
    public class AddTargetLanguageRequest
    {
        public string ProjectId { get; set; }

        public IEnumerable<string> TargetLanguages { get; set; }
    }
}
