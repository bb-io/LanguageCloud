using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Dtos
{
    public class TranslationMemoryDto
    {
        public string UId { get; set; }

        public string Name { get; set; }

        public string SourceLang { get; set; }

        public IEnumerable<string> TargetLangs { get; set; }
    }
}
