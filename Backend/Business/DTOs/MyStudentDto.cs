using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class MyStudentDto
    {
        public int RelationId { get; set; }
        public string RelationType { get; set; } = string.Empty;
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? UniqueNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? LanguageLevel { get; set; }
    }
}
