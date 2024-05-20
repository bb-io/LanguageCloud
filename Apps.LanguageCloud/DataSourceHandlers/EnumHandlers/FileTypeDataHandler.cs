using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class FileTypeDataHandler : IStaticDataSourceHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"native", "Native"},
            {"bcm", "BCM"},
            {"sdlxliff", "SDL XLIFF" }
        };
        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
