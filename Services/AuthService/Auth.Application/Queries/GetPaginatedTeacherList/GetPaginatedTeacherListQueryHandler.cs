using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using Shared.Application.Models;

namespace Auth.Application.Queries.GetPaginatedTeacherList
{
    public class GetPaginatedTeacherListQueryHandler : IRequestHandler<GetPaginatedTeacherListQuery, PaginatedResult<UserEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedTeacherListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<UserEntity>> Handle(GetPaginatedTeacherListQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Users.Where(u => u.Role == UserRole.Учитель).OrderByDescending(c => c.CreatedAt);

            var items = await query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PaginatedResult<UserEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
