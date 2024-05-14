using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

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
