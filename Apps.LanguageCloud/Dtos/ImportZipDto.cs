using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Dtos
{
    public class ImportZipDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string UnzipStatus { get; set; }
        public IEnumerable<AssociatedFile> AssociatedFiles { get; set; }
    }

    public class AssociatedFile
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
