using System.ComponentModel.DataAnnotations;

namespace Course.Domain.Models
{
    public class LessonEntity
    {
        [Key]
        public Guid LessonId { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
