using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    // Matricula digital minima; el resto lo completa el Tutor despues (fuera de este alcance).
    public class CreateStudentDto
    {
        [Required]
        public int GroupId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }
        public string? UniqueNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
    }
}
