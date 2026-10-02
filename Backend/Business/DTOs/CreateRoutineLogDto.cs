using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateRoutineLogDto
    {
        [Required]
        public int RoutineDetailId { get; set; }

        [Required]
        public int StudentId { get; set; }

        public string? Status { get; set; }
        public string? Observation { get; set; }
        public DateTime? LogDate { get; set; }
    }
}
