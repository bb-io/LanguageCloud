using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class TaskDto
{
    [Display("Task ID")]
    public string Id { get; set; }

    [Display("Status")]
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

    [Display("Input")]
    public Input input { get; set; }
  
}

public class Input
{
    [Display("Input type")]
    public string type { get; set; }
    
    [Display("Source file")]
    public SourceFile sourceFile { get; set; }
    
    [Display("Target file")]
    public TargetFile targetFile { get; set; }
    
    [Display("Language direction")]
    public LanguageDirection languageDirection { get; set; }
}

public class SourceFile
{
    [Display("Source file ID")]
    public string id { get; set; }
    
    [Display("File name")]
    public string name { get; set; }
    
    [Display("Role")]
    public string role { get; set; }
}

public class TargetFile
{
    [Display("Target file ID")]
    public string id { get; set; }
    
    [Display("File name")]
    public string name { get; set; }
    
    [Display("Language direction")]
    public LanguageDirection languageDirection { get; set; }
}