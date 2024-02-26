using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Tasks.Responses
{
    public class TaskResponse
    {
        [Display("Task ID")]
        public string Id { get; set; }
        public string Status { get; set; }

        [Display("Project ID")]
        public string ProjectID { get; set; }

        [Display("Project Name")]
        public string ProjectName { get; set; }

        [Display("Task Type ID")]
        public string TaskTypeID { get; set; }

        [Display("Task Type Name")]
        public string TaskTypeName { get; set; }

        [Display("Task Type Key")]
        public string TaskTypeKey { get; set; }

        [Display("Task Type Description")]
        public string TaskTypeDescription { get; set; }
        [Display("Source Language")]
        public string SourceLanguage { get; set; }
        [Display("Target Language")]
        public string TargetLanguage { get; set; }

        [Display("Source File ID")]
        public string SourceFileID { get; set; }

        [Display("Source File Name")]
        public string SourceFileName { get; set; }

        [Display("Target File ID")]
        public string TargetFileID { get; set; }

        [Display("Target File Name")]
        public string TargetFileName { get; set; }


    }
}
