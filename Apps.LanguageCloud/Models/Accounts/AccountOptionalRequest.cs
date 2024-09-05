using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageCloud.Models.Accounts;

public class AccountOptionalRequest
{
    [Display("Account ID"), DataSource(typeof(AccountDataSource))]
    public string? AccountId { get; set; }
}