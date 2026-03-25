using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Tasks.Responses;

public record ListAllTasksResponse(List<TaskDto> Tasks);