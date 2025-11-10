using Auth.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Queries.GetPaginatedTeacherList
{
    public record GetPaginatedTeacherListQuery(int Page, int PageSize) : IRequest<List<UserEntity>>
    {
    }
}
