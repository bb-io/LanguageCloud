using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class UserTypeDataHandler : IStaticDataSourceHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"user", "User"},
            {"group", "Group"},
            {"vendorOrderTemplate", "Vendor Order Template" },
            {"projectManager", "Project Manager"},
            {"projectCreator", "Project Creator"}
        }; 
        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
