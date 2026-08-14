using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public MessageCodes MessageCodes { get; set; }
        public string? Message { get; set; }
        public object? Details { get; set; }
    }
}
