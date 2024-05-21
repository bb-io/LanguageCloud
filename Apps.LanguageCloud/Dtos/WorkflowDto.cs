using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Dtos
{
    public class WorkflowDto
    {
        [Display("Workflow ID")]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
