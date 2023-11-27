using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class FileTypeDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            {"native", "Native"},
            {"bcm", "BCM"},
            {"sdlxliff", "SDL XLIFF" }
        };
    }
}
