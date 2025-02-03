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
}
