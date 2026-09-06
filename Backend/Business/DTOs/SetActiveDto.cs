using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    // Reutilizable para activar/desactivar ClassGroup o Student
    public class SetActiveDto
    {
        public bool IsActive { get; set; }
    }
}
