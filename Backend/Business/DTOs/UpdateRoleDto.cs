using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateRoleDto
    {
        [Required(ErrorMessage = "El nombre del rol es requerido")]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
