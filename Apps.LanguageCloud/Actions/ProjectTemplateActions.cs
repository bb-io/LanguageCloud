using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.ProjectTemplates.Requests;
using Apps.LanguageCloud.Models.ProjectTemplates.Responses;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class ProjectTemplateActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("List all project templates", Description = "List all project templates")]
    public async Task<ListAllProjectsTemplatesResponse> ListAllProjectTemplates()
    {
        var request = new LanguageCloudRequest("/project-templates", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<ProjectTemplateDto>>>(request);
        return new ListAllProjectsTemplatesResponse()
        {
            ProjectTemplates = response.Items
        };
    }

    [Action("Get project template", Description = "Get project template by Id")]
    public async Task<ProjectTemplateDto?> GetProjectTemplate([ActionParameter] GetProjectTemplateRequest input)
    {
        var request = new LanguageCloudRequest($"/project-templates/{input.Template}", Method.Get);
        return await Client.ExecuteWithErrorHandling<ProjectTemplateDto>(request);
    }
}