using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

using Order.Domain.Models;
using Shared.Infrastructure;

namespace Order.Application.Command.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddToCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.Services
                .FirstOrDefaultAsync(s => s.ServiceId == request.ServiceId, cancellationToken);

            if (service == null)
                throw new Exception("Услуга не найдена");

            var existingItem = await _unitOfWork.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == request.UserId && ci.ServiceId == request.ServiceId, cancellationToken);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                existingItem.Price = existingItem.Quantity * service.Price;
            }
            else
            {
                var newItem = new CartItemEntity
                {
                    CartItemId = Guid.NewGuid(),
                    UserId = request.UserId,
                    ServiceId = service.ServiceId,
                    Quantity = request.Quantity,
                    Price = service.Price * request.Quantity
                };

                await _unitOfWork.CartItems.AddAsync(newItem, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return request.UserId;
        }
    }
}
