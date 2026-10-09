using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateStudyHistoryDto
    {
        [Required(ErrorMessage = "El estudiante (StudentId) es requerido")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "La materia (SubjectId) es requerida")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "La lección (LessonId) es requerida")]
        public int LessonId { get; set; }

        public decimal? Score { get; set; }
        public int? StudyTime { get; set; }
        public DateTime? StudyDate { get; set; }
        public string? Result { get; set; }
        public string? Difficulty { get; set; }
    }
}
