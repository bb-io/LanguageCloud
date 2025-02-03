using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Tasks.Requests;
using Apps.LanguageCloud.Models.Tasks.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System.Net;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class TaskActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("Get project tasks", Description = "Get tasks related to a project")]
    public async Task<ListAllTasksResponse> GetProjectTasks([ActionParameter] ListAllProjectTasksRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}/tasks?fields=id,status,taskType,project,input", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<TaskDto>>>(request);
        return new ListAllTasksResponse()
        {
            Tasks = response.Items
        };
    }

    [Action("Get task", Description = "Get task by ID")]
    public async Task<TaskResponse> GetTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}?fields=id,status,taskType,project,input", Method.Get);
        var task =  await Client.ExecuteWithErrorHandling<TaskDto>(request);
        string tgt ="";
        string src = "";
        string tgtfile = "";
        string tgtfilename = "";
        string srcfile = "";
        string srcfilename = "";
        if (task.input.languageDirection != null && task.input.languageDirection != null) 
            { tgt = task.input.languageDirection.TargetLanguage.LanguageCode;
                src = task.input.languageDirection.SourceLanguage.LanguageCode;
            } else if (task.input.targetFile != null)
            {
                tgt = task.input.targetFile.languageDirection.TargetLanguage.LanguageCode;
                src = task.input.targetFile.languageDirection.SourceLanguage.LanguageCode;
            }

        if (task.input.targetFile != null)
        { tgtfile = task.input.targetFile.id;
          tgtfilename = task.input.targetFile.name;
        }
        if (task.input.sourceFile != null)
        {
            srcfile = task.input.sourceFile.id;
            srcfilename = task.input.sourceFile.name;
        }

        return new TaskResponse
        {
            Id = task.Id,
            ProjectID = task.Project.Id,
            ProjectName = task.Project.Name,
            Status = task.Status,
            TaskTypeID = task.TaskType.Id,
            TaskTypeKey = task.TaskType.Key,
            TaskTypeDescription = task.TaskType.Description,
            TaskTypeName = task.TaskType.Name,
            SourceLanguage = src,
            TargetLanguage = tgt,
            SourceFileID = srcfile,
            TargetFileID = tgtfile,
            SourceFileName = srcfilename,
            TargetFileName = tgtfilename
        };
    }

    [Action("Accept task", Description = "Accept task by ID")]
    public async Task AcceptTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/accept", Method.Put);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Reject task", Description = "Reject task by ID")]
    public async Task RejectTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/reject", Method.Put);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Complete task", Description = "Complete task by ID")]
    public async Task CompleteTask([ActionParameter] CompleteTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/complete", Method.Put);
        request.AddJsonBody(new
        {
            outcome = "done",
            comment = input.Comment
        });

        try
        {
            await Client.ExecuteWithErrorHandling(request);
        }
        catch (HttpRequestException ex)
        {
            var message = ex.StatusCode switch
            {
                HttpStatusCode.Conflict => "The task is not in 'created' or 'in progress' state",
                _ => ex.Message
            };

            throw new PluginApplicationException(message);
        }
    }

    [Action("Release task", Description = "Release task by ID")]
    public async Task ReleaseTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/release", Method.Put);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Reclaim task", Description = "Reclaim task by ID")]
    public async Task ReclaimTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/reclaim", Method.Put);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Assign task", Description = "Assign task by ID")]
    public async Task AssignTask([ActionParameter] AssignTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/assign", Method.Put);
        request.AddJsonBody(new
        {
            assignees = new[] {
                new {
                    id = input.AssigneeId,
                    type = input.Type
                }
            }
        });
        await Client.ExecuteWithErrorHandling(request);
    }
}