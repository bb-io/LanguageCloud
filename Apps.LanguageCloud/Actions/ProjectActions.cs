using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using Apps.LanguageCloud.Models.Projects.Responses;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Responses;
using Apps.LanguageCloud.Models.Projects.Requests;
using Blackbird.Applications.Sdk.Common.Invocation;
using System.Globalization;

namespace Apps.LanguageCloud.Actions;

[ActionList("Projects")]
public class ProjectActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("Search projects", Description = "Search for projects given certain filters")]
    public async Task<ListAllProjectsResponse> SearchProjects([ActionParameter] SearchProjectsRequest input)
    {
        var request = new LanguageCloudRequest("/projects", Method.Get);
        request.AddQueryParameter("fields", "id,shortId,name,description,dueBy,createdAt,status,languageDirections");

        if (input.CreatedFrom.HasValue)
            request.AddQueryParameter("createdFrom", input.CreatedFrom.Value.ToString("o", CultureInfo.InvariantCulture));

        if (input.CreatedTo.HasValue)
            request.AddQueryParameter("createdTo", input.CreatedTo.Value.ToString("o", CultureInfo.InvariantCulture));

        if (input.DueFrom.HasValue)
            request.AddQueryParameter("dueFrom", input.DueFrom.Value.ToString("o", CultureInfo.InvariantCulture));

        if (input.DueTo.HasValue)
            request.AddQueryParameter("dueTo", input.DueTo.Value.ToString("o", CultureInfo.InvariantCulture));

        if (input.ExcludeOnline.HasValue)
            request.AddQueryParameter("excludeOnline", input.ExcludeOnline.Value.ToString());

        if (input.Location != null)
            request.AddQueryParameter("location", input.Location);

        if (input.ProjectName != null)
            request.AddQueryParameter("projectName", input.ProjectName);

        if (input.ProjectTemplateId != null)
            request.AddQueryParameter("projectTemplateId", input.ProjectTemplateId);

        if (input.SourceLanguage != null)
            request.AddQueryParameter("sourceLanguage", input.SourceLanguage);

        if (input.TargetLanguage != null)
            request.AddQueryParameter("targetLanguage", input.TargetLanguage);

        if (input.Status != null)
            request.AddQueryParameter("status", input.Status);

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
        return await Client.ExecuteWithErrorHandling<ProjectDto>(request)!;
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

    [Action("Update project", Description = "Edit project")]
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
}