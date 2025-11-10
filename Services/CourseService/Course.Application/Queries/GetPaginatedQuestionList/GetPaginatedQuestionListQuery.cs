using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetPaginatedQuestionList
{
    public record GetPaginatedQuestionListQuery(int Page, int PageSize) : IRequest<List<QuestionEntity>>
    {
    }
}
