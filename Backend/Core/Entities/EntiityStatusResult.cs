using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EntityStatusResult
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string EntityStatus { get; set; } = string.Empty;
        public DateTime? DismissalDate { get; set; }
        public string? DismissalReason { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
