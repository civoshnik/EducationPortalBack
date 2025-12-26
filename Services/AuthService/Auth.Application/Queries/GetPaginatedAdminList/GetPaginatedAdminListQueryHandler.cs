using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Queries.GetPaginatedAdminList
{
    public class GetPaginatedAdminListQueryHandler : IRequestHandler<GetPaginatedAdminListQuery, PaginatedResult<UserEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedAdminListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<UserEntity>> Handle(GetPaginatedAdminListQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Users.Where(u => u.Role == UserRole.Администратор).OrderByDescending(c => c.CreatedAt);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PaginatedResult<UserEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
