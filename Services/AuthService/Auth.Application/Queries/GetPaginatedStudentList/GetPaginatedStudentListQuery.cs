using Auth.Domain.Models;
using MediatR;

namespace Auth.Application.Queries.GetPaginatedStudentList
{
    public record GetPaginatedStudentListQuery(int Page, int PageSize) : IRequest<List<UserEntity>>
    {

    }
}
