using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RoutineLog
    {
        public int Id { get; set; }
        public int RoutineDetailId { get; set; }
        public int StudentId { get; set; }
        public string? Status { get; set; }
        public string? Observation { get; set; }
        public DateTime? LogDate { get; set; }
        public int? RegisteredById { get; set; }
    }
}
