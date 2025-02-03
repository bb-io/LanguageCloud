using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Dtos;
public class ErrorDto
{
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public IEnumerable<Detail> Details { get; set; }
}

public class Detail
{
    public string Name { get; set; }
}