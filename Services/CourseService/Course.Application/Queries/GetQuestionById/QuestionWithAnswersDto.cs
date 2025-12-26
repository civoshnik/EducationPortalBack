using Course.Domain.Models;

public class QuestionWithAnswersDto
{
    public Guid QuestionId { get; set; }
    public Guid TestId { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

    public List<QuestionAnswerEntity> Answers { get; set; } = new();
}
