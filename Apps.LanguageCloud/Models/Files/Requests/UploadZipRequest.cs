using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Files.Requests
{
    public class UploadZipRequest
    {
        public byte[] File { get; set; }

        public string FileName { get; set; }
    }
}
