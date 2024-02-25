namespace Apps.LanguageCloud.Webhooks.Payload;

public class TaskEvent
{
    public string id { get; set; }
    public string status { get; set; }
    public string taskInfrastructureStatus { get; set; }
    public string type { get; set; }
    public string typeId { get; set; }
    public bool automatic { get; set; }
    public string inputType { get; set; }
    public string inputId { get; set; }
    public string? output { get; set; }
    public string? outcome { get; set; }
    public object applicableOutcomes { get; set; }
    public object outcomes { get; set; }
    public object instructions { get; set; }
    public string? comment { get; set; }
    public string phaseId { get; set; }
    public Phase phase { get; set; }
    public string location { get; set; }
    public object permissions { get; set; }
    public object accessGrants { get; set; }
    public object path { get; set; }
    public Owner? owner { get; set; }
    public List<PlannedAssignee>? plannedAssignees { get; set; }
    public List<Assignee>? assignees { get; set; }
    public List<InputFile> inputFiles { get; set; }
    public string projectId { get; set; }
    public List<object> accountUsersWhoRejected { get; set; }
    public DateTime? dueDate { get; set; }
    public DateTime? dueBy { get; set; }
    public Settings settings { get; set; }
    public Metadata metadata { get; set; }
    public int batch { get; set; }
    public DateTime lastModifiedAt { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? completedAt { get; set; }
    public Template template { get; set; }
    public string scope { get; set; }
    public int iteration { get; set; }
}

public class Assignee
{
    public string type { get; set; }
    public string id { get; set; }
}

public class InputFile
{
    public string fileId { get; set; }
    public string fileVersionId { get; set; }
    public string type { get; set; }
    public object fileReferenceId { get; set; }
    public object supportedFileTypeSettingIds { get; set; }
}

public class Metadata
{
    public int numberOfFiles { get; set; }
    public bool forceOnline { get; set; }
    public DateTime markedLatestFromtrueOn { get; set; }
    public DateTime lastModifiedDate { get; set; }
    public string? projectDescription { get; set; }
    public string TR_ID { get; set; }
    public string projectName { get; set; }
    public DateTime creationDate { get; set; }
    public int version { get; set; }
    public string markedLatestFromtrueTraceId { get; set; }
}

public class Owner
{
    public string type { get; set; }
    public string id { get; set; }
}

public class Phase
{
    public string id { get; set; }
    public object name { get; set; }
    public object description { get; set; }
}

public class PlannedAssignee
{
    public string type { get; set; }
    public string id { get; set; }
}

public class Settings
{
    public DateTime lastModifiedDate { get; set; }
    public DateTime creationDate { get; set; }
}

public class Template
{
    public string name { get; set; }
    public string businessKey { get; set; }
    public string phaseId { get; set; }
    public string skippingStrategy { get; set; }
}

