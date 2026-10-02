using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class LearningContentDto
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Level { get; set; }
        public bool IsActive { get; set; }
        public List<string> Keywords { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}
