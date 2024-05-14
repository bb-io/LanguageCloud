using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

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
