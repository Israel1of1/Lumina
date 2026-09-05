using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{

    public class UpdateClassGroupDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? GradeLevel { get; set; }
        public string? Description { get; set; }
    }
}
