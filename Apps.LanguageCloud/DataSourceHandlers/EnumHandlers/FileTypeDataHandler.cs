using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class FileTypeDataHandler : IStaticDataSourceItemHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"native", "Native"},
            {"bcm", "BCM"},
            {"sdlxliff", "SDLXLIFF" }
        };
        public IEnumerable<DataSourceItem> GetData()
        {
            return EnumValues.Select(x => new DataSourceItem(x.Key, x.Value));
        }
    }
}
