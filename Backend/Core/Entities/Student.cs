using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UniqueNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? LanguageLevel { get; set; }
        public string? ClinicalInfo { get; set; }
        public string? Observations { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
