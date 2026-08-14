using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        public string? NationalId { get; set; }

        public bool IsActive { get; set; }
    }
}
