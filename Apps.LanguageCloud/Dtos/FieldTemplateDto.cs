using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Dtos
{
    public class FieldTemplateDto
    {
        [Display("Field Template ID")]
        public string Id { get; set; }

        [Display("Field Template Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
