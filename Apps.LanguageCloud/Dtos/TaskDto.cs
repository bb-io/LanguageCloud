using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;
using System.Diagnostics;

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

    public Input input { get; set; }
  
}

public class Input
{
    public string type { get; set; }
    public SourceFile sourceFile { get; set; }
    public TargetFile targetFile { get; set; }
    public LanguageDirection languageDirection { get; set; }
}
public class SourceFile
{
    public string id { get; set; }
    public string name { get; set; }
    public string role { get; set; }
}

public class TargetFile
{
    public string id { get; set; }
    public string name { get; set; }
    public LanguageDirection languageDirection { get; set; }
}