using Apps.LanguageCloud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Files.Responses
{
    public class ListAllFilesResponse
    {
        public IEnumerable<FileInfoDto> Files { get; set; }
    }
}
