using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Responses
{
    public class ResponseWrapper<T>
    {
        public T Items { get; set; }
    }
}
