using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.Models.Files.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.LanguageCloud.Base;

namespace Tests.LanguageCloud;

[TestClass]
public class FileTests : TestBase
{
    [TestMethod]
    public async Task Download_target_file_works()
    {
        var actions = new FileActions(InvocationContext, FileManager);

        var result = await actions.DownloadTargetFile(new DownloadFileRequest { FileId = "671abfd637ba3219444fba16", Format = "sdlxliff", ProjectId = "671abfc837ba3219444fac2a" });
        Assert.IsNotNull(result.File.Name);

    }

    [TestMethod]
    public async Task Get_target_file_info_works()
    {
        var actions = new FileActions(InvocationContext, FileManager);

        var result = await actions.GetTargetFile(new GetFileRequest { FileId = "671ac0635db7ac3bf6b438ca",  ProjectId = "671ac0568dcd3660baa78da6" });
        Console.WriteLine($"{result.Name} - {result.Id}");
        Assert.IsNotNull(result.Id);

    }
}
