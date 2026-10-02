using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UpdateStudentHabitDto
    {
        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }
        public string? Name { get; set; }
        public string? Frequency { get; set; }
        public string? Observations { get; set; }
    }
}
