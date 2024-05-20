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
public class ProjectTemplateActions : LanguageCloudInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public ProjectTemplateActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    [Action("List all project templates", Description = "List all project templates")]
    public ListAllProjectsTemplatesResponse ListAllProjectTemplates()
    {
        var request = new LanguageCloudRequest("/project-templates", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<ProjectTemplateDto>>>(request);
        return new ListAllProjectsTemplatesResponse()
        {
            ProjectTemplates = response.Items
        };
    }

    [Action("Get project template", Description = "Get project template by Id")]
    public ProjectTemplateDto? GetProjectTemplate([ActionParameter] GetProjectTemplateRequest input)
    {
        var request = new LanguageCloudRequest($"/project-templates/{input.Template}", Method.Get, Creds);
        return Client.Get<ProjectTemplateDto>(request);
    }
}