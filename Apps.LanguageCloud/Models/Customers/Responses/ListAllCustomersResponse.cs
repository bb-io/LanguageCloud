using Apps.LanguageCloud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Customers.Responses
{
    public class ListAllCustomersResponse
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
    }
}
