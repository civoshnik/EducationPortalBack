using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Queries.GetPaginatedTeacherList
{
    public class GetPaginatedTeacherListQueryHandler : IRequestHandler<GetPaginatedTeacherListQuery, List<UserEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedTeacherListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<UserEntity>> Handle(GetPaginatedTeacherListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Users
                .Where(u => u.Role == UserRole.Учитель)
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
