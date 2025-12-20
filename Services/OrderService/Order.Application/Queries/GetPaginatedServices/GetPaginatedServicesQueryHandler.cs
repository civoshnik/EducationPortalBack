using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using Shared.Application.Models;

namespace Order.Application.Queries
{
    public class GetPaginatedServiceQueryHandler : IRequestHandler<GetPaginatedServicesQuery, PaginatedResult<ServiceEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedServiceQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<ServiceEntity>> Handle(GetPaginatedServicesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Services.OrderByDescending(s => s.CreatedAt);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PaginatedResult<ServiceEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

}
