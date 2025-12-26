using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new OrderEntity
            {
                OrderId = Guid.NewGuid(),
                UserId = request.UserId,
                Status = OrderStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = 0
            };

            await _unitOfWork.Orders.AddAsync(order, cancellationToken);

            decimal total = 0;

            foreach (var s in request.Services)
            {
                var orderService = new OrderServiceEntity
                {
                    OrderId = order.OrderId,
                    ServiceId = s.ServiceId,
                    Quantity = s.Quantity,
                    TotalPrice = s.Price * s.Quantity
                };

                total += orderService.TotalPrice;

                await _unitOfWork.OrderServices.AddAsync(orderService, cancellationToken);
            }
            order.TotalPrice = total;

            var items = await _unitOfWork.CartItems
                .Where(ci => ci.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            _unitOfWork.CartItems.RemoveRange(items);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order.OrderId;
        }

    }
}
