using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class QuoteFormatDataHandler : IStaticDataSourceHandler
    {
        private static Dictionary<string, string> EnumValues => new()
          {
                {"excel","Excel" },
                {"pdf","PDF" }
          };
        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}