using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Tasks.Requests;
using Apps.LanguageCloud.Models.Tasks.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System.Net;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class TaskActions
{
    [Action("List all tasks", Description = "List all tasks")]
    public ListAllTasksResponse ListAllTasks(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest("/tasks/assigned?fields=id,status,taskType,project,input", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<TaskDto>>>(request);
        return new ListAllTasksResponse()
        {
            Tasks = response.Items
        };
    }

    [Action("List all project tasks", Description = "List all project tasks")]
    public ListAllTasksResponse ListAllProjectTasks(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, 
        [ActionParameter] ListAllProjectTasksRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/projects/{input.Project}/tasks?fields=id,status,taskType,project,input", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<TaskDto>>>(request);
        return new ListAllTasksResponse()
        {
            Tasks = response.Items
        };
    }

    [Action("Get task", Description = "Get task by Id")]
    public TaskResponse GetTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetTaskRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/tasks/{input.Task}?fields=id,status,taskType,project,input", Method.Get, authenticationCredentialsProviders);
        var task =  client.Get<TaskDto>(request);
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

    [Action("Accept task", Description = "Accept task by Id")]
    public void AcceptTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetTaskRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/accept", Method.Put, authenticationCredentialsProviders);
        client.Execute(request);
    }

    [Action("Reject task", Description = "Reject task by Id")]
    public void RejectTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetTaskRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/reject", Method.Put, authenticationCredentialsProviders);
        client.Execute(request);
    }

    [Action("Complete task", Description = "Complete task by Id")]
    public void CompleteTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CompleteTaskRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/complete", Method.Put, authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            outcome = "done",
            comment = input.Comment
        });

        try
        {
            client.Execute(request);
        }
        catch (HttpRequestException ex)
        {
            var message = ex.StatusCode switch
            {
                HttpStatusCode.Conflict => "The task is not in created or in progress state",
                _ => ex.Message
            };

            throw new(message);
        }
    }

    [Action("Release task", Description = "Release task by Id")]
    public void ReleaseTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetTaskRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/release", Method.Put, authenticationCredentialsProviders);
        client.Execute(request);
    }

    [Action("Reclaim task", Description = "Reclaim task by Id")]
    public void ReclaimTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetTaskRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/reclaim", Method.Put, authenticationCredentialsProviders);
        client.Execute(request);
    }

    [Action("Assign task", Description = "Assign task by Id")]
    public void AssignTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] AssignTaskRequest input)
    {
        var client = new LanguageCloudClient();
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/assign", Method.Put, authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            assignees = new[] {
                new {
                    id = input.AssigneeId,
                    type = input.Type
                }
            }
        });
        client.Execute(request);
    }
}