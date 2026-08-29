using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class CreateLessonDto
    {
        [Required(ErrorMessage = "El módulo (ModuleId) es requerido")]
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "El título de la lección es requerido")]
        [MaxLength(150, ErrorMessage = "El título no puede exceder los 150 caracteres")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Type { get; set; }

        public int? DurationMinutes { get; set; }
    }
}