using System.ComponentModel.DataAnnotations;

namespace Course.Domain.Models
{
    public class CourseEntity
    {
        [Key]
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public int DurationHours { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsPublished { get; set; }
    }
}
