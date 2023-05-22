using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Tasks.Requests;
using Apps.LanguageCloud.Models.Tasks.Responses;
using Apps.LanguageCloud.Models.Users.Requests;
using Apps.LanguageCloud.Models.Users.Responses;
using Apps.LanguageCloud.Webhooks.Payload;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Actions
{
    [ActionList]
    public class TaskActions
    {
        [Action("List all tasks", Description = "List all tasks")]
        public ListAllTasksResponse ListAllTasks(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest("/tasks/assigned", Method.Get, authenticationCredentialsProviders);
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
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/projects/{input.ProjectId}/tasks", Method.Get, authenticationCredentialsProviders);
            var response = client.Get<ResponseWrapper<List<TaskDto>>>(request);
            return new ListAllTasksResponse()
            {
                Tasks = response.Items
            };
        }

        [Action("Get task", Description = "Get task by Id")]
        public TaskDto? GetTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetTaskRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/tasks/{input.Id}?fields=status", Method.Get, authenticationCredentialsProviders);
            return client.Get<TaskDto>(request);
        }

        [Action("Accept task", Description = "Accept task by Id")]
        public void AcceptTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetTaskRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/tasks/{input.Id}/accept", Method.Put, authenticationCredentialsProviders);
            client.Execute(request);
        }

        [Action("Reject task", Description = "Reject task by Id")]
        public void RejectTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetTaskRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/tasks/{input.Id}/reject", Method.Put, authenticationCredentialsProviders);
            client.Execute(request);
        }

        [Action("Complete task", Description = "Complete task by Id")]
        public void CompleteTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] CompleteTaskRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/tasks/{input.Id}/complete", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                outcome = input.Outcome,
                comment = input.Comment
            });
            client.Execute(request);
        }

        [Action("Release task", Description = "Release task by Id")]
        public void ReleaseTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetTaskRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/tasks/{input.Id}/release", Method.Put, authenticationCredentialsProviders);
            client.Execute(request);
        }

        [Action("Reclaim task", Description = "Reclaim task by Id")]
        public void ReclaimTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetTaskRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/tasks/{input.Id}/reclaim", Method.Put, authenticationCredentialsProviders);
            client.Execute(request);
        }

        [Action("Assign task", Description = "Assign task by Id")]
        public void AssignTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] AssignTaskRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/tasks/{input.TaskId}/assign", Method.Put, authenticationCredentialsProviders);
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
}
