using MediatR;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Order.Application.Queries.GetUserOrders
{
    internal class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, List<OrderEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }

        public async Task<List<OrderEntity>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders
                .Where(u => u.UserId == request.UserId)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}
