using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateRoutineDetailDto
    {
        [Required]
        public int RoutineId { get; set; }
        public TimeSpan? TimeOfDay { get; set; }
        public string? Activity { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
