using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class FileFormatDataHandler : IStaticDataSourceItemHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"TMX", "TMX"},
            {"XLSX", "Excel file"}
        };
        public IEnumerable<DataSourceItem> GetData()
        {
            return EnumValues.Select(x => new DataSourceItem(x.Key, x.Value));
        }
    }
}
