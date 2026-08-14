using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UpdateSubjectDto
    {
        [Required(ErrorMessage = "El nombre de la materia es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [MaxLength(20)]
        public string? Color { get; set; }

        [MaxLength(100)]
        public string? Icon { get; set; }
    }
}
