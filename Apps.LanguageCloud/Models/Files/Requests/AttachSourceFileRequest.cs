using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Files.Requests
{
    public class AttachSourceFileRequest
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string FileId { get; set; }

        public string LanguageCode { get; set; }
    }
}
