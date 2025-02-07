﻿using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Tasks.Requests;

public class AssignTaskRequest
{
    [Display("Task ID")]
    public string Task { get; set; }

    [Display("Assignee ID")]
    public string AssigneeId { get; set; }

    [Display("User type")]
    [StaticDataSource(typeof(UserTypeDataHandler))]
    public string Type { get; set; }
}