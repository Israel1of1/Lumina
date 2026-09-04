using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateGuardianProfileDto
    {
      
        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }
        public string? NationalId { get; set; }
        public string? PersonalEmail { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Photo { get; set; }
        public string? Relationship { get; set; }
    }
}
