using Auth.Application.Queries.GetPaginatedStudentList;
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

namespace Auth.Application.Queries.GetPaginatedBlackList
{
    public class GetPaginatedBlackListQueryHandler : IRequestHandler<GetPaginatedBlackListQuery, PaginatedResult<BlacklistUserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedBlackListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }

        public async Task<PaginatedResult<BlacklistUserDto>> Handle(GetPaginatedBlackListQuery request, CancellationToken cancellationToken)
        {
            var query = from b in _unitOfWork.Blacklist
                        join u in _unitOfWork.Users on b.UserId equals u.UserId
                        select new BlacklistUserDto
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Login = u.Login,
                            Email = u.Email,
                            Role = (int)u.Role,
                            Phone = u.Phone,
                            BlacklistedAt = b.BlacklistedAt
                        };

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PaginatedResult<BlacklistUserDto>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

}
