using Course.Domain.Models;
using MediatR;
using Shared.Application.Models;

namespace Course.Application.Queries.GetPaginatedTestList
{
    public record GetPaginatedTestListQuery(int Page, int PageSize) : IRequest<PaginatedResult<TestEntity>>
    {
    }
}
