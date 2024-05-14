using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.LanguageCloud.Models.Glossaries.Responses
{
    public class ExportGlossaryResponse
    {
        [Display("Glossary")]
        public FileReference File { get; set; }
    }
}
