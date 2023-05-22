using Apps.LanguageCloud.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Tasks.Responses
{
    public class ListAllTasksResponse
    {
        public List<TaskDto> Tasks { get; set; }
    }
}
