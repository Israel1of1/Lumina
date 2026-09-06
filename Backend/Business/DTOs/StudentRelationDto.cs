using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class StudentRelationDto
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public string RelationType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? EndDate { get; set; }
        public string? EntityFirstName { get; set; }
        public string? EntityLastName { get; set; }
    }
}
