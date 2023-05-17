using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Files.Requests
{
    public class GetFileRequest
    {
        public string ProjectId { get; set; }

        public string FileId { get; set; }
    }
}
