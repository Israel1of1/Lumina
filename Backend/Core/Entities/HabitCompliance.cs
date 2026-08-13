using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class HabitCompliance
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public DateTime ComplianceDate { get; set; }
        public bool IsFulfilled { get; set; }
        public string? Observation { get; set; }
        public int? RegisteredById { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
