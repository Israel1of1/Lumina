using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EntityStudentRelation
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public int StudentId { get; set; }
        public string RelationType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
