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
public class CustomerActions(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [Action("List all customers", Description = "List all customers")]
    public async Task<ListAllCustomersResponse> ListAllCustomers()
    {
        
        var request = new LanguageCloudRequest("/customers", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<CustomerDto>>>(request);
        return new ListAllCustomersResponse()
        {
            Customers = response.Items
        };
    }

    [Action("Get customer", Description = "Get customer by Id")]
    public async Task<CustomerDto?> GetCustomer([ActionParameter] GetCustomerRequest input)
    {
        
        var request = new LanguageCloudRequest($"/customers/{input.Id}", Method.Get);
        return await Client.ExecuteWithErrorHandling<CustomerDto>(request);
    }
}