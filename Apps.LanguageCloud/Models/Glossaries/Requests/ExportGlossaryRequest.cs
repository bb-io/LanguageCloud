using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Glossaries.Requests
{
    public class ExportGlossaryRequest
    {
        [Display("Glossary ID")]
        [DataSource(typeof(TermBaseDataHandler))]
        public string GlossaryId { get; set; }
    }
}
