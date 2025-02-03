using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models;
public class OptionalProjectStatusRequest
{
    [Display("Status")]
    [StaticDataSource(typeof(ProjectStatusDataHandler))]
    public string? Status { get; set; }
}
