﻿using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageCloud.DataSourceHandlers.EnumHandlers
{
    public class FileRoleDataHandler : IStaticDataSourceHandler
    {
        private static Dictionary<string, string> EnumValues => new()
        {
            {"translatable", "Translatable"},
            {"reference", "Reference"},
            {"unknown", "Unknown" }
        };
        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
