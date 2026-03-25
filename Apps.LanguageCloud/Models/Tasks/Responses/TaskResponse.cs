using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Tasks.Responses;

public class TaskResponse(
    TaskDto taskDto, 
    string srcLang, 
    string targLang, 
    string srcFileId,
    string targFileId,
    string srcFileName, 
    string targFileName)
{
    [Display("Task ID")]
    public string Id { get; set; } = taskDto.Id;

    [Display("Status")]
    public string Status { get; set; } = taskDto.Status;

    [Display("Due by")]
    public DateTime? DueBy { get; set; } = taskDto.DueBy;

    [Display("Created at")]
    public DateTime? CreatedAt { get; set; } = taskDto.CreatedAt;

    [Display("Completed at")]
    public DateTime? CompletedAt { get; set; } = taskDto.CompletedAt;

    [Display("Project ID")]
    public string ProjectID { get; set; } = taskDto.Project.Id;

    [Display("Project name")]
    public string ProjectName { get; set; } = taskDto.Project.Name;

    [Display("Task type ID")]
    public string TaskTypeID { get; set; } = taskDto.TaskType.Id;

    [Display("Task type name")]
    public string TaskTypeName { get; set; } = taskDto.TaskType.Name;

    [Display("Task type key")]
    public string TaskTypeKey { get; set; } = taskDto.TaskType.Key;

    [Display("Task type description")]
    public string TaskTypeDescription { get; set; } = taskDto.TaskType.Description;

    [Display("Source language")]
    public string SourceLanguage { get; set; } = srcLang;

    [Display("Target language")]
    public string TargetLanguage { get; set; } = targLang;

    [Display("Source file ID")]
    public string SourceFileID { get; set; } = srcFileId;

    [Display("Source file name")]
    public string SourceFileName { get; set; } = srcFileName;

    [Display("Target file ID")]
    public string TargetFileID { get; set; } = targFileId;

    [Display("Target file name")]
    public string TargetFileName { get; set; } = targFileName;

    [Display("Assignees")]
    public IEnumerable<TaskAssigneeResponse> Assignees { get; set; } = taskDto.Assignees.Select(x => new TaskAssigneeResponse(x));

    [Display("Vendor order ID")]
    public string? VendorOrderId { get; set; } = taskDto.input.Order?.Id;

    [Display("Quote total translation fee")]
    public double? VendorOrderTotalAmount { get; set; } = taskDto.input.Order?.Quote.TotalAmount;

    [Display("Quote currency code")]
    public string? VendorOrderCurrencyCode { get; set; } = taskDto.input.Order?.Quote.CurrencyCode;

    [Display("Quote notes")]
    public string? VendorOrderQuoteNotes { get; set; } = taskDto.input.Order?.Quote.Notes;
}
