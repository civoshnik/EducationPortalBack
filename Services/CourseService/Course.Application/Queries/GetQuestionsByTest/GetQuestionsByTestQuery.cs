using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetQuestionsByTest
{
    public record GetQuestionsByTestQuery(Guid TestId) : IRequest<List<QuestionEntity>>;
}
