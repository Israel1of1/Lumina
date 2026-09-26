using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? UniqueNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? LanguageLevel { get; set; }
        public string? ClinicalInfo { get; set; }
        public string? Observations { get; set; }
        public bool IsActive { get; set; }
        public bool HasAccount { get; set; }
    }
}
