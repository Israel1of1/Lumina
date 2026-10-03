using System;

namespace Business.DTOs
{
    public class StudentInterestDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}