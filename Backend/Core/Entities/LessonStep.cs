using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LessonStep
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int? StepNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ContentType { get; set; }
        public string? ContentUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
