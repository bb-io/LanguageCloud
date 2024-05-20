using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos
{
    public class FileProcessingConfigurationDto
    {
        [Display("File Processing Configuration ID")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
