using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using Apps.LanguageCloud.Models.Projects.Responses;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Projects.Requests;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class ProjectActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    [Action("List all projects", Description = "List all projects")]
    public ListAllProjectsResponse ListAllProjects()
    {
        var request = new LanguageCloudRequest("/projects?fields=" +
            "id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<ProjectDto>>>(request);
        return new ListAllProjectsResponse()
        {
            Projects = response.Items
        };
    }

    [Action("Get project", Description = "Get project by Id")]
    public ProjectDto GetProject(
        [ActionParameter] GetProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}?fields=" +
            $"id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get, Creds);
        return Client.Get<ProjectDto>(request);
    }

    [Action("Create project", Description = "Create project")]
    public ProjectDto CreateProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateProjectRequest input)
    {
        var request = new LanguageCloudRequest("/projects", Method.Post, authenticationCredentialsProviders);

        // temp solution for sync from localize. Need convert operator on array to remove this workaround
        input.SourceLanguage = input.SourceLanguage.Replace('_', '-');
        input.TargetLanguages = input.TargetLanguages.Select(t => t.Replace('_', '-')).ToList(); 
        // ----------------------------------------------------------------------------------------------
        
        request.AddStringBody(input.GetSerializedRequest(), DataFormat.Json);
        return Client.Post<ProjectDto>(request);
    }

    [Action("Create project from template", Description = "Create project from template")]
    public ProjectDto CreateProjectFromTemplate([ActionParameter] CreateFromTemplateRequest input)
    {
        var request = new LanguageCloudRequest($"/projects", Method.Post, Creds);
        request.AddStringBody(input.GetSerializedRequest(), DataFormat.Json);
        return Client.Post<ProjectDto>(request);
    }

    [Action("Edit project", Description = "Edit project")]
    public void EditProject([ActionParameter] EditProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}", Method.Put, Creds);
        request.AddJsonBody(new
        {
            name = input.ProjectName,
        });
        Client.Execute(request);
    }

    [Action("Delete project", Description = "Delete project")]
    public void DeleteProject([ActionParameter] DeleteProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}", Method.Delete, Creds);
        Client.Execute(request);
    }

    [Action("Start project", Description = "Start project by Id")]
    public void StartProject([ActionParameter] GetProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}/start", Method.Put, Creds);
        Client.Execute(request);
    }

    [Action("Complete project", Description = "Complete project by Id")]
    public void CompleteProject([ActionParameter] GetProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}/complete", Method.Put, Creds);
        Client.Execute(request);
    }

    [Action("List all languages", Description = "List all languages")]
    public ListAllLanguagesResponse ListAllLanguages()
    {
        var request = new LanguageCloudRequest("/languages", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<LanguageDto>>>(request);
        return new ListAllLanguagesResponse()
        {
            Languages = response.Items
        };
    }
}