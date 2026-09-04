using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
   
        public class PagedResultDto<T>
        {
            public List<T> Items { get; set; } = new();
            public int TotalRecords { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }
    
}
