using Apps.LanguageCloud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Users.Responses
{
    public class ListAllUsersResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
