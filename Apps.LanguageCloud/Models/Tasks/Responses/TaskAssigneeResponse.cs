using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Tasks.Responses;

public class TaskAssigneeResponse(TaskAssigneeDto dto)
{
    [Display("Assignee type")]
    public string AssigneeType { get; set; } = dto.Type;

    [Display("Assignee ID")]
    public string? AssigneeId { get; set; } = dto.Type switch
    {
        "user" => dto.User?.Id,
        "group" => dto.Group?.Id,
        "vendorOrderTemplate" => dto.VendorOrderTemplate?.Id,
        _ => dto.User?.Id ?? dto.Group?.Id ?? dto.VendorOrderTemplate?.Id
    };

    [Display("Assignee email")]
    public string? AssigneeEmail { get; set; } = dto.User?.Email;

    [Display("Assignee first name")]
    public string? AssigneeFirstName { get; set; } = dto.User?.FirstName;

    [Display("Assignee last name")]
    public string? AssigneeLastName { get; set; } = dto.User?.LastName;

    [Display("Assignee group name")]
    public string? AssigneeGroupName { get; set; } = dto.Group?.Name;
}
