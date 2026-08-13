using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StudentProgress
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal? CompletionPercentage { get; set; }
        public string? CurrentLevel { get; set; }
        public string? Strengths { get; set; }
        public string? Weaknesses { get; set; }
        public string? Recommendation { get; set; }
        public int? TotalStudyTime { get; set; }
        public DateTime? LastSessionAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
