using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetPaginatedTestList
{
    public class GetPaginatedTestListQueryHandler : IRequestHandler<GetPaginatedTestListQuery, PaginatedResult<TestEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedTestListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }

        public async Task<PaginatedResult<TestEntity>> Handle(GetPaginatedTestListQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Tests.OrderByDescending(c => c.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<TestEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
