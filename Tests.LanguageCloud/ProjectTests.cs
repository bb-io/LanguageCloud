using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.Connections;
using Apps.LanguageCloud.Models.Projects.Requests;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.LanguageCloud.Base;

namespace Tests.LanguageCloud;

[TestClass]
public class ProjectTests : TestBase
{
    [TestMethod]
    public async Task Search_projects_works()
    {
        var actions = new ProjectActions(InvocationContext);

        var result = await actions.ListAllProjects(new SearchProjectsRequest { ExcludeOnline = true });
        Console.WriteLine($"Total: {result.Projects.Count()}");
        foreach (var project in result.Projects)
        {
            Console.WriteLine(project.Name);
        }
        Assert.IsTrue(result.Projects.Count() > 0);
    }

    [TestMethod]
    public async Task Get_project_works()
    {
        var actions = new ProjectActions(InvocationContext);

        var result = await actions.GetProject(new GetProjectRequest { Project = "664ca028795d716598e7bfde" });
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Assert.IsNotNull(result.Id);
    }
}