using MediatR;

namespace Course.Application.Commands.CreateQuestion
{
    public record CreateQuestionCommand : IRequest
    {
        public Guid TestId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; }
    }
}

