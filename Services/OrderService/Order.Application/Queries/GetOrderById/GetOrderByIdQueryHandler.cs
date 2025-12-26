namespace Order.Application.Queries.GetOrderById
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Order.Domain.Models;
    using Shared.Application.Interfaces;

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderWithServicesDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderWithServicesDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders
                .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null) return null!;

            var services = await _unitOfWork.OrderServices
                .Where(os => os.OrderId == request.OrderId)
                .ToListAsync(cancellationToken);

            return new OrderWithServicesDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                ModifiedAt = order.ModifiedAt,
                Services = services
            };
        }
    }

}

