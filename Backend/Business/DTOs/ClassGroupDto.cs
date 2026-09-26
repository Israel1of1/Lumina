using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ClassGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? GradeLevel { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int StudentCount { get; set; }
        public int AvailableSlots => 10 - StudentCount;
    }
}
