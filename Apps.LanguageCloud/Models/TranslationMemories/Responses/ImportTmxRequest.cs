using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.TranslationMemories.Responses
{
    public class ImportTmxRequest
    {
        public string TranslationMemoryId { get; set; }

        public byte[] File { get; set; }

        public string Filename { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }
    }
}
