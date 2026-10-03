using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateStudentProgressDto
    {
        public decimal? CompletionPercentage { get; set; }
        public string? CurrentLevel { get; set; }
        public string? Strengths { get; set; }
        public string? Weaknesses { get; set; }
        public string? Recommendation { get; set; }
        public int? TotalStudyTime { get; set; }
        public DateTime? LastSessionAt { get; set; }
    }
}
