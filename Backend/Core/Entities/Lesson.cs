using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public int? DurationMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
