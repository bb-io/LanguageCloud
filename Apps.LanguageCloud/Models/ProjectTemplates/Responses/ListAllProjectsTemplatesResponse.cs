using Apps.LanguageCloud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.ProjectTemplates.Responses
{
    public class ListAllProjectsTemplatesResponse
    {
        public IEnumerable<ProjectTemplateDto> ProjectTemplates { get; set; }
    }
}
