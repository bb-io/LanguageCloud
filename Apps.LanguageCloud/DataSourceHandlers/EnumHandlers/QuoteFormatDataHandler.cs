using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class QuoteFormatDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
          {
                {"excel","Excel" },
                {"pdf","PDF" }
          };
    }
}