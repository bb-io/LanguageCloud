﻿using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Projects.Responses;

public class ListAllProjectsResponse
{
    public IEnumerable<ProjectDto> Projects { get; set; }
}