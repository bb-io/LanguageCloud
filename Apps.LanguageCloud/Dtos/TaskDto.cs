using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class TaskDto
{
    public string Id { get; set; }

    public string Status { get; set; }

    //[Display("Due by")]
    //[JsonProperty("dueBy")]
    //public DateTime DueBy { get; set; }

    //[Display("Created at")]
    //[JsonProperty("createdAt")]
    //public DateTime CreatedAt { get; set; }

    [Display("Task type")]
    [JsonProperty("taskType")]
    public TaskType TaskType { get; set; }

    [Display("Project")]
    [JsonProperty("project")]
    public ProjectDto Project { get; set; }
}