using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;
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
    public async Task List_projects_works()
    {
        var actions = new ProjectActions(InvocationContext);

        var result = await actions.ListAllProjects();
        Console.WriteLine($"Total: {result.Projects.Count()}");
        foreach (var project in result.Projects)
        {
            Console.WriteLine(project.Name);
        }
        Assert.IsTrue(result.Projects.Count() > 0);
    }
}