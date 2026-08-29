using System;

namespace Business.DTOs
{
    public class LessonStepDto
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int? StepNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ContentType { get; set; }
        public string? ContentUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}