using MediatR;

namespace Course.Application.Commands.DeleteQuestion
{
    public record DeleteQuestionCommand : IRequest
    {
        public Guid QuestionId { get; set; }
    }
}
