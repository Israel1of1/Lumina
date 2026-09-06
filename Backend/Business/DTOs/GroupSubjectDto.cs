using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class GroupSubjectDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string? TeacherFirstName { get; set; }
        public string? TeacherLastName { get; set; }
        public string? GroupName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
