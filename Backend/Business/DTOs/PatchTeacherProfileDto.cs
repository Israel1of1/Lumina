using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
  
        public class PatchTeacherProfileDto
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? NationalId { get; set; }
            public string? PersonalEmail { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? Photo { get; set; }
            public string? Specialty { get; set; }
            public string? Degree { get; set; }
        }
    
}

