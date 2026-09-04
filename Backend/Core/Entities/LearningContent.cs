using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LearningContent
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public bool IsDictionary { get; set; }
        public bool IsRoutine { get; set; }
        public bool IsException { get; set; }
        public int? SubjectId { get; set; }
        public string? Level { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
