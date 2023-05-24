using Apps.LanguageCloud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Projects.Responses
{
    public class ListAllLanguagesResponse
    {
        public IEnumerable<LanguageDto> Languages { get; set; }
    }
}
