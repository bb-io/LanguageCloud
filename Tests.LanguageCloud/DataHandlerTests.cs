using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.Models.Files.Requests;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.LanguageCloud.Base;

namespace Tests.LanguageCloud;

[TestClass]
public class DataHandlerTests : TestBase
{
    [TestMethod]
    public async Task Get_projects_works()
    {
        var handler = new ProjectDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);
        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task Get_locations_works()
    {
        var handler = new LocationDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);
        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task Get_project_templates_works()
    {
        var handler = new ProjectTemplateDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);
        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task Get_languages_works()
    {
        var handler = new LanguageDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);
        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task Get_translation_memories_works()
    {
        var handler = new TranslationMemoryDataSource(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);
        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }
}
