using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class CreateStudentInterestDto
    {
        [Required(ErrorMessage = "El estudiante (StudentId) es requerido")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "El nombre del interés es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}