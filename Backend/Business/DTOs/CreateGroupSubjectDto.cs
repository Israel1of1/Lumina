using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateGroupSubjectDto
    {
        [Required]
        public int GroupId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public DateTime? AssignmentDate { get; set; }
    }
}
