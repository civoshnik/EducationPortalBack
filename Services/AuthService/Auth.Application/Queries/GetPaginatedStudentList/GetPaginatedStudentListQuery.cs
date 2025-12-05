using Auth.Domain.Models;
using MediatR;
using Shared.Application.Models;

namespace Auth.Application.Queries.GetPaginatedStudentList
{
    public record GetPaginatedStudentListQuery(int Page, int PageSize) : IRequest<PaginatedResult<UserEntity>>
    {

    }
}
