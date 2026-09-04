using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public object? Meta { get; set; }
    }
}
