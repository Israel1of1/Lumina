using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateStudentRelationDto
    {
        [Required]
        public int EntityId { get; set; }

        [Required]
        public string EntityType { get; set; } = string.Empty; // TEACHER | GUARDIAN

        [Required]
        public int StudentId { get; set; }

        [Required]
        public string RelationType { get; set; } = string.Empty; // REFERRING_TEACHER | GUARDIAN | PRIMARY_GUARDIAN

        public DateTime? AssignedAt { get; set; }
    }
}
