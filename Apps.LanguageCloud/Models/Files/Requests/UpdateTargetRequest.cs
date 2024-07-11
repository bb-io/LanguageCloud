using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;


namespace Apps.LanguageCloud.Models.Files.Requests
{
    public class UpdateTargetRequest
    {
        [DataSource(typeof(ProjectDataHandler))]
        [Display("Project ID")]
        public string ProjectId { get; set; }

        [Display("File ID")]
        public string FileId { get; set; }

        public FileReference File { get; set; }
    }
}
