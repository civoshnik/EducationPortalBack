using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Auth.Application.Queries.GetPaginatedStudentList
{
    public class GetPaginatedStudentListQueryHandler : IRequestHandler<GetPaginatedStudentListQuery, List<UserEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedStudentListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserEntity>> Handle(GetPaginatedStudentListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Users
                .Where(u => u.Role == UserRole.Ученик)
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }
    } 
}
