using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class FileRoleDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            {"translatable", "Translatable"},
            {"reference", "Reference"},
            {"unknown", "Unknown" }
        };
    }
}
