﻿using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class ProjectStatusDataHandler : IStaticDataSourceItemHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"created", "Created"},
            {"inProgress", "In progress"},
            {"completed", "Completed" },
            {"archived", "Archived"},
        };
        public IEnumerable<DataSourceItem> GetData()
        {
            return EnumValues.Select(x => new DataSourceItem(x.Key, x.Value));
        }
    }
}
