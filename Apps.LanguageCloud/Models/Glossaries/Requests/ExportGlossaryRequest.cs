using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Glossaries.Requests
{
    public class ExportGlossaryRequest
    {
        [Display("Glossary")]
        [DataSource(typeof(TermBaseDataHandler))]
        public string GlossaryId { get; set; }
    }
}
