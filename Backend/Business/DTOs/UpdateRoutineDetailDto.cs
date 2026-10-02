using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateRoutineDetailDto
    {
        public TimeSpan? TimeOfDay { get; set; }
        public string? Activity { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
