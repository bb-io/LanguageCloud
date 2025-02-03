using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class UserTypeDataHandler : IStaticDataSourceItemHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"user", "User"},
            {"group", "Group"},
            {"vendorOrderTemplate", "Vendor Order Template" },
            {"projectManager", "Project Manager"},
            {"projectCreator", "Project Creator"}
        };
        public IEnumerable<DataSourceItem> GetData()
        {
            return EnumValues.Select(x => new DataSourceItem(x.Key, x.Value));
        }
    }
}
