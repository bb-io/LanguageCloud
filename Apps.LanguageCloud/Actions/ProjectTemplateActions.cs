using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Projects.Requests;
using Apps.LanguageCloud.Models.Projects.Responses;
using Apps.LanguageCloud.Models.ProjectTemplates.Requests;
using Apps.LanguageCloud.Models.ProjectTemplates.Responses;
using Apps.LanguageCloud.Models.Responses;
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
    public class ProjectTemplateActions
    {
        [Action("List all project templates", Description = "List all project templates")]
        public ListAllProjectsTemplatesResponse ListAllProjectTemplates(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest("/project-templates", Method.Get, authenticationCredentialsProviders);
            var response = client.Get<ResponseWrapper<List<ProjectTemplateDto>>>(request);
            return new ListAllProjectsTemplatesResponse()
            {
                ProjectTemplates = response.Items
            };
        }

        [Action("Get project template", Description = "Get project template by Id")]
        public ProjectTemplateDto? GetProjectTemplate(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetProjectTemplateRequest input)
        {
            var client = new LanguageCloudClient(authenticationCredentialsProviders);
            var request = new LanguageCloudRequest($"/project-templates/{input.Id}", Method.Get, authenticationCredentialsProviders);
            return client.Get<ProjectTemplateDto>(request);
        }
    }
}
