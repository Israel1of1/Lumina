using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StudentHabit
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }
        public string? Name { get; set; }
        public string? Frequency { get; set; }
        public string? Observations { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
