using Course.Domain.Models;
using MediatR;
using Shared.Application.Models;

namespace Course.Application.Queries.GetPaginatedQuestionList
{
    public record GetPaginatedQuestionListQuery(int Page, int PageSize) : IRequest<PaginatedResult<QuestionEntity>>
    {
    }
}
