using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class StrategyDataHandler : IStaticDataSourceHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"Copy", "copy"},
            {"Use", "use"},
        };
        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
