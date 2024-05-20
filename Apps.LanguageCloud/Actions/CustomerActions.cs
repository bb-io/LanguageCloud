using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models.Customers.Requests;
using Apps.LanguageCloud.Models.Customers.Responses;
using Apps.LanguageCloud.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageCloud.Actions;

[ActionList]
public class CustomerActions : LanguageCloudInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public CustomerActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List all customers", Description = "List all customers")]
    public ListAllCustomersResponse ListAllCustomers()
    {
        
        var request = new LanguageCloudRequest("/customers", Method.Get, Creds);
        var response = Client.Get<ResponseWrapper<List<CustomerDto>>>(request);
        return new ListAllCustomersResponse()
        {
            Customers = response.Items
        };
    }

    [Action("Get customer", Description = "Get customer by Id")]
    public CustomerDto? GetCustomer([ActionParameter] GetCustomerRequest input)
    {
        
        var request = new LanguageCloudRequest($"/customers/{input.Id}", Method.Get, Creds);
        return Client.Get<CustomerDto>(request);
    }
}