using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Tasks.Responses;

public class TaskAssigneeResponse(TaskAssigneeDto dto)
{
    [Display("Assignee type")]
    public string AssigneeType { get; set; } = dto.Type;

    [Display("Assignee user ID")]
    public string AssigneeId { get; set; } = dto.User.Id;

    [Display("Assignee email")]
    public string AssigneeEmail { get; set; } = dto.User.Email;

    [Display("Assignee first name")]
    public string AssigneeFirstName { get; set; } = dto.User.FirstName;

    [Display("Assignee last name")]
    public string AssigneeLastName { get; set; } = dto.User.LastName;
}
