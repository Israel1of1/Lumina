using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class SetRoutineStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty; // ACTIVE | COMPLETED | CANCELLED
    }
}
