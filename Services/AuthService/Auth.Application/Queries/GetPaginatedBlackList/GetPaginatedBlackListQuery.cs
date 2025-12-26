using Auth.Domain.Models;
using MediatR;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Queries.GetPaginatedBlackList
{
    public record GetPaginatedBlackListQuery(int Page, int PageSize) : IRequest<PaginatedResult<BlacklistUserDto>>
    {
    }
}
