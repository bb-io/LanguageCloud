using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Quotes.Requests
{
    public class DownloadQuoteReportRequest
    {
        public string ProjectId { get; set; }

        public string LanguageCode { get; set; }

        public string FileFormat { get; set; } // pdf or excel
    }
}
