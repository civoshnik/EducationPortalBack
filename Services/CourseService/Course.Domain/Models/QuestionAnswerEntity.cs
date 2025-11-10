using System.ComponentModel.DataAnnotations;

namespace Course.Domain.Models
{
    public class QuestionAnswerEntity
    {
        [Key]
        public Guid QuestionAnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
