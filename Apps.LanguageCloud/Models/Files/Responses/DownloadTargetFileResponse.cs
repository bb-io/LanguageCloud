using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Files.Responses
{
    public class DownloadTargetFileResponse
    {
        public string FileName { get; set; }

        public byte[] File { get; set; }
    }
}
