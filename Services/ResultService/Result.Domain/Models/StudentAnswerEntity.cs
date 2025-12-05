using System.ComponentModel.DataAnnotations;

namespace Result.Domain.Models
{
    public class StudentAnswerEntity
    {
        [Key]
        public Guid StudentAnswerId { get; set; }
        public Guid TestingProgressId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid QuestionAnswerId { get; set; }
        public DateTime AnswerAt { get; set; }
    }
}
