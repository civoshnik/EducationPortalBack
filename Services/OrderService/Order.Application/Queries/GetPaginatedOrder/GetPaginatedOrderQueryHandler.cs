using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using Shared.Application.Models;

namespace Order.Application.Queries.GetPaginatedOrder
{
    public class GetPaginatedOrderQueryHandler : IRequestHandler<GetPaginatedOrderQuery, PaginatedResult<OrderEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<OrderEntity>> Handle(GetPaginatedOrderQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Orders.OrderByDescending(c => c.CreatedAt);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PaginatedResult<OrderEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
