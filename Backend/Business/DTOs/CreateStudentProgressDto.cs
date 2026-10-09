using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateStudentProgressDto
    {
        [Required(ErrorMessage = "El estudiante (StudentId) es requerido")]
        public int StudentId { get; set; }

        public decimal? CompletionPercentage { get; set; }
        public string? CurrentLevel { get; set; }
        public string? Strengths { get; set; }
        public string? Weaknesses { get; set; }
        public string? Recommendation { get; set; }
        public int? TotalStudyTime { get; set; }
        public DateTime? LastSessionAt { get; set; }
    }
}
