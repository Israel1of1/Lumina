using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreatePecsCardDto
    {
        [Required]
        public int BoardId { get; set; }

        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public string? Category { get; set; }
        public int? OrderNumber { get; set; }
    }
}
