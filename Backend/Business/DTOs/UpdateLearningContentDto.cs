using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateLearningContentDto
    {
        [Range(1, int.MaxValue)]
        public int LessonId { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required, MaxLength(20)]
        public string Type { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int? SubjectId { get; set; }

        [MaxLength(20)]
        public string? Level { get; set; }
    }
}
