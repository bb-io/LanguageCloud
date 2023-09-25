using Apps.LanguageCloud.Dtos;

namespace Apps.LanguageCloud.Models.Customers.Responses;

public class ListAllCustomersResponse
{
    public IEnumerable<CustomerDto> Customers { get; set; }
}