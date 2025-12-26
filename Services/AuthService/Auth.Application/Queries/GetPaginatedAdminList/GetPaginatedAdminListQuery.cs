using Auth.Domain.Models;
using MediatR;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Queries.GetPaginatedAdminList
{
   public record GetPaginatedAdminListQuery(int Page, int PageSize) : IRequest<PaginatedResult<UserEntity>>
    {
    }
}
