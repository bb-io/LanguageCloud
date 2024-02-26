using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class UserTypeDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            {"user", "User"},
            {"group", "Group"},
            {"vendorOrderTemplate", "Vendor Order Template" },
            {"projectManager", "Project Manager"},
            {"projectCreator", "Project Creator"}
        };
    }
}
