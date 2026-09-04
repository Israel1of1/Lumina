using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateModuleDto
    {
        [Required(ErrorMessage = "La materia (SubjectId) es requerida")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "El nombre del módulo es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [MaxLength(255)]
        public string? IconUrl { get; set; }
    }
}
