using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using Apps.LanguageCloud.Models.Projects.Responses;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Projects.Requests;
using Apps.LanguageCloud.Webhooks.Payload;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class ProjectActions
{
    [Action("List all projects", Description = "List all projects")]
    public ListAllProjectsResponse ListAllProjects(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest("/projects?fields=" +
            "id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<ProjectDto>>>(request);
        return new ListAllProjectsResponse()
        {
            Projects = response.Items
        };
    }

    [Action("Get project", Description = "Get project by Id")]
    public ProjectDto GetProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetProjectRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.Project}?fields=" +
            $"id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get, authenticationCredentialsProviders);
        return client.Get<ProjectDto>(request);
    }

    [Action("Create project", Description = "Create project")]
    public ProjectDto CreateProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateProjectRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest("/projects", Method.Post, authenticationCredentialsProviders);

        // temp solution for sync from localize. Need convert operator on array to remove this workaround
        input.SourceLanguage = input.SourceLanguage.Replace('_', '-');
        input.TargetLanguages = input.TargetLanguages.Select(t => t.Replace('_', '-')).ToList(); 
        // ----------------------------------------------------------------------------------------------
        
        request.AddStringBody(input.GetSerializedRequest(), DataFormat.Json);
        return client.Post<ProjectDto>(request);
    }

    [Action("Create project from template", Description = "Create project from template")]
    public ProjectDto CreateProjectFromTemplate(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateFromTemplateRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects", Method.Post, authenticationCredentialsProviders);
        request.AddStringBody(input.GetSerializedRequest(), DataFormat.Json);
        return client.Post<ProjectDto>(request);
    }

    [Action("Edit project", Description = "Edit project")]
    public void EditProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] EditProjectRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.Project}", Method.Put, authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            name = input.ProjectName,
        });
        client.Execute(request);
    }

    [Action("Delete project", Description = "Delete project")]
    public void DeleteProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] DeleteProjectRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.Project}", Method.Delete, authenticationCredentialsProviders);
        client.Execute(request);
    }

    [Action("Start project", Description = "Start project by Id")]
    public void StartProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetProjectRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.Project}/start", Method.Put, authenticationCredentialsProviders);
        client.Execute(request);
    }

    [Action("Complete project", Description = "Complete project by Id")]
    public void CompleteProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetProjectRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/projects/{input.Project}/complete", Method.Put, authenticationCredentialsProviders);
        client.Execute(request);
    }

    [Action("List all languages", Description = "List all languages")]
    public ListAllLanguagesResponse ListAllLanguages(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest("/languages", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<LanguageDto>>>(request);
        return new ListAllLanguagesResponse()
        {
            Languages = response.Items
        };
    }
}