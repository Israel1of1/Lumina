using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreatePecsBoardDto
    {
     
            [Required]
            public int StudentId { get; set; }

            public string? Name { get; set; }
            public string? Description { get; set; }
        
    }
}
