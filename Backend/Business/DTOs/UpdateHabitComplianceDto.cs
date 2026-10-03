using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateHabitComplianceDto
    {
        [Required(ErrorMessage = "El hábito (HabitId) es requerido")]
        public int HabitId { get; set; }

        [Required(ErrorMessage = "La fecha de cumplimiento es requerida")]
        public DateTime ComplianceDate { get; set; }

        public bool IsFulfilled { get; set; }

        public string? Observation { get; set; }

        public int? RegisteredById { get; set; }
    }
}
