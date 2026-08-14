using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UnsuccessfulResponseDto
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object? Details { get; set; }
    }
}
