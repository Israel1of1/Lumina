using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StudyHistory
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int LessonId { get; set; }
        public decimal? Score { get; set; }
        public int? StudyTime { get; set; }
        public DateTime? StudyDate { get; set; }
        public string? Result { get; set; }
        public string? Difficulty { get; set; }
    }
}
