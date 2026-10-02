using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateKeywordDto
    {
        [Required, MaxLength(100)]
        [RegularExpression(@"^[^,]+$", ErrorMessage = "El nombre no puede contener comas.")]
        public string Name { get; set; } = string.Empty;
    }
}
