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
    [Action("List all projects", Description = "List all projects")]
    public async Task<ListAllProjectsResponse> ListAllProjects()
    {
        var request = new LanguageCloudRequest("/projects?fields=" +
            "id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<ProjectDto>>>(request);
        return new ListAllProjectsResponse()
        {
            Projects = response.Items
        };
    }

    [Action("Get project", Description = "Get project by ID")]
    public async Task<ProjectDto> GetProject([ActionParameter] GetProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}?fields=" +
            $"id,shortId,name,description,dueBy,createdAt,status,languageDirections", Method.Get);
        var project = await Client.ExecuteWithErrorHandling<ProjectDto>(request)!;
        project.GroupLanguageDirections();
        
        return project;
    }

    [Action("Create project", Description = "Create project")]
    public async Task<ProjectDto> CreateProject([ActionParameter] CreateProjectRequest input)
    {
        var request = new LanguageCloudRequest("/projects", Method.Post);

        // temp solution for sync from localize. Need convert operator on array to remove this workaround
        input.SourceLanguage = input.SourceLanguage.Replace('_', '-');
        input.TargetLanguages = input.TargetLanguages.Select(t => t.Replace('_', '-')).ToList(); 
        // ----------------------------------------------------------------------------------------------
        
        request.AddStringBody(input.GetSerializedRequest(), DataFormat.Json);
        return await Client.ExecuteWithErrorHandling<ProjectDto>(request);
    }

    [Action("Create project from template", Description = "Create project from template")]
    public async Task<ProjectDto> CreateProjectFromTemplate([ActionParameter] CreateFromTemplateRequest input)
    {
        var request = new LanguageCloudRequest($"/projects", Method.Post);
        request.AddStringBody(input.GetSerializedRequest(), DataFormat.Json);
        return await Client.ExecuteWithErrorHandling<ProjectDto>(request);
    }

    [Action("Edit project", Description = "Edit project")]
    public async Task EditProject([ActionParameter] EditProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}", Method.Put);
        request.AddJsonBody(new
        {
            name = input.ProjectName,
        });
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Delete project", Description = "Delete project")]
    public async Task DeleteProject([ActionParameter] DeleteProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}", Method.Delete);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Start project", Description = "Start project by Id")]
    public async Task StartProject([ActionParameter] GetProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}/start", Method.Put);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Complete project", Description = "Complete project by Id")]
    public async Task CompleteProject([ActionParameter] GetProjectRequest input)
    {
        var request = new LanguageCloudRequest($"/projects/{input.Project}/complete", Method.Put);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("List all languages", Description = "List all languages")]
    public async Task<ListAllLanguagesResponse> ListAllLanguages()
    {
        var request = new LanguageCloudRequest("/languages", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<LanguageDto>>>(request);
        return new ListAllLanguagesResponse()
        {
            Languages = response.Items
        };
    }
}