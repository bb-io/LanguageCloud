using Apps.LanguageCloud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Groups.Responses
{
    public class ListAllGroupsResponse
    {
        public IEnumerable<GroupDto> Groups { get; set; }
    }
}
