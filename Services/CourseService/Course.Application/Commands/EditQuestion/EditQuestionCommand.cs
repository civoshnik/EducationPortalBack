using MediatR;

namespace Course.Application.Commands.EditQuestion
{
    public record EditQuestionCommand : IRequest
    {
        public Guid QuestionId { get; set; }
        public Guid TestId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }
}
