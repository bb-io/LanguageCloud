using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Tasks.Responses
{
    public class TaskResponse
    {
        [Display("Task ID")]
        public string Id { get; set; }
        public string Status { get; set; }

        [Display("Project ID")]
        public string ProjectID { get; set; }

        [Display("Project name")]
        public string ProjectName { get; set; }

        [Display("Task type ID")]
        public string TaskTypeID { get; set; }

        [Display("Task type name")]
        public string TaskTypeName { get; set; }

        [Display("Task type key")]
        public string TaskTypeKey { get; set; }

        [Display("Task type description")]
        public string TaskTypeDescription { get; set; }
        
        [Display("Source language")]
        public string SourceLanguage { get; set; }
        
        [Display("Target language")]
        public string TargetLanguage { get; set; }

        [Display("Source file ID")]
        public string SourceFileID { get; set; }

        [Display("Source file name")]
        public string SourceFileName { get; set; }

        [Display("Target file ID")]
        public string TargetFileID { get; set; }

        [Display("Target file name")]
        public string TargetFileName { get; set; }
    }
}
