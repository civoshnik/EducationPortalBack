using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetQuestionById
{
    public record GetQuestionByIdQuery : IRequest<QuestionWithAnswersDto>
    {
        public Guid QuestionId { get; set; }
    }
}
