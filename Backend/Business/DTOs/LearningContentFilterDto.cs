using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class LearningContentFilterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LessonId { get; set; }
        public int? SubjectId { get; set; }
        public string? Type { get; set; }
        public string? Level { get; set; }
        public string? Search { get; set; }   // busca en titulo, descripcion y palabras clave
        public bool? IsActive { get; set; }
    }
}
