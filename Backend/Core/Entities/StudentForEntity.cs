using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    // Estudiante visto desde la perspectiva de un Teacher/Guardian (via USP_GetActiveStudentsForEntity)
    public class StudentForEntity
    {
        public int RelationId { get; set; }
        public string RelationType { get; set; } = string.Empty;
        public DateTime? AssignedAt { get; set; }
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? UniqueNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? LanguageLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
