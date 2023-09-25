using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Customers.Requests;
using Apps.LanguageCloud.Models.Customers.Responses;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class CustomerActions
{
    [Action("List all customers", Description = "List all customers")]
    public ListAllCustomersResponse ListAllCustomers(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest("/customers", Method.Get, authenticationCredentialsProviders);
        var response = client.Get<ResponseWrapper<List<CustomerDto>>>(request);
        return new ListAllCustomersResponse()
        {
            Customers = response.Items
        };
    }

    [Action("Get customer", Description = "Get customer by Id")]
    public CustomerDto? GetCustomer(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetCustomerRequest input)
    {
        var client = new LanguageCloudClient(authenticationCredentialsProviders);
        var request = new LanguageCloudRequest($"/customers/{input.Id}", Method.Get, authenticationCredentialsProviders);
        return client.Get<CustomerDto>(request);
    }
}