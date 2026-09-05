using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TeacherWithAccount
    {
            public Teacher Teacher { get; set; } = null!;
            public string? AccountEmail { get; set; }
            public bool? AccountIsActive { get; set; }
        
    }
}
