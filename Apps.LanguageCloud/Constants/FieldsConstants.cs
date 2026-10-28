namespace Apps.LanguageCloud.Constants;

public static class FieldsConstants
{
    public const string Task = "id,status,taskType,project,input,assignees,dueBy,createdAt,completedAt";

    public const string PackageExportTask =
        "id,status,taskType.name,taskType.automatic,taskType.scope,input.type,input.targetFile,assignees";
}
