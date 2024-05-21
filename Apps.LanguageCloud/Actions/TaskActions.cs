using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Tasks.Requests;
using Apps.LanguageCloud.Models.Tasks.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System.Net;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class TaskActions : LanguageCloudInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public TaskActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List all tasks", Description = "List all tasks")]
    public ListAllTasksResponse ListAllTasks()
    {
        var request = new LanguageCloudRequest("/tasks/assigned?fields=id,status,taskType,project,input", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<TaskDto>>>(request);
        return new ListAllTasksResponse()
        {
            Tasks = response.Items
        };
    }

    [Action("List all project tasks", Description = "List all project tasks")]
    public ListAllTasksResponse ListAllProjectTasks([ActionParameter] ListAllProjectTasksRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}/tasks?fields=id,status,taskType,project,input", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<TaskDto>>>(request);
        return new ListAllTasksResponse()
        {
            Tasks = response.Items
        };
    }

    [Action("Get task", Description = "Get task by Id")]
    public TaskResponse GetTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}?fields=id,status,taskType,project,input", Method.Get, Creds);
        var task =  Client.Get<TaskDto>(request);
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
    public void AcceptTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/accept", Method.Put, Creds);
        Client.Execute(request);
    }

    [Action("Reject task", Description = "Reject task by Id")]
    public void RejectTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/reject", Method.Put, Creds);
        Client.Execute(request);
    }

    [Action("Complete task", Description = "Complete task by Id")]
    public void CompleteTask([ActionParameter] CompleteTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/complete", Method.Put, Creds);
        request.AddJsonBody(new
        {
            outcome = "done",
            comment = input.Comment
        });

        try
        {
            Client.Execute(request);
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
    public void ReleaseTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/release", Method.Put, Creds);
        Client.Execute(request);
    }

    [Action("Reclaim task", Description = "Reclaim task by Id")]
    public void ReclaimTask([ActionParameter] GetTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/reclaim", Method.Put, Creds);
        Client.Execute(request);
    }

    [Action("Assign task", Description = "Assign task by Id")]
    public void AssignTask([ActionParameter] AssignTaskRequest input)
    {
        var request = new LanguageCloudRequest($"/tasks/{input.Task}/assign", Method.Put, Creds);
        request.AddJsonBody(new
        {
            assignees = new[] {
                new {
                    id = input.AssigneeId,
                    type = input.Type
                }
            }
        });
        Client.Execute(request);
    }
}