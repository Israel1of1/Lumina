using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class CreateLessonStepDto
    {
        [Required(ErrorMessage = "La lección (LessonId) es requerida")]
        public int LessonId { get; set; }

        public int? StepNumber { get; set; }

        [MaxLength(150, ErrorMessage = "El título no puede exceder los 150 caracteres")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [MaxLength(50)]
        public string? ContentType { get; set; }

        [MaxLength(500)]
        public string? ContentUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}