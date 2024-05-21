using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class FileFormatDataHandler : IStaticDataSourceHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"TMX", "TMX"},
            {"XLSX", "Excel file"}
        };
        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
