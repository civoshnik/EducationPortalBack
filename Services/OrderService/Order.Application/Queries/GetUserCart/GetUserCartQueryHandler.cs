using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Queries.GetUserCart
{
    public class GetUserCartQueryHandler : IRequestHandler<GetUserCartQuery, List<CartItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserCartQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CartItemDto>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.CartItems
                .Where(ci => ci.UserId == request.UserId)
                .Join(_unitOfWork.Services,
                      ci => ci.ServiceId,
                      s => s.ServiceId,
                      (ci, s) => new CartItemDto
                      {
                          CartItemId = ci.CartItemId,
                          UserId = ci.UserId,
                          ServiceId = ci.ServiceId,
                          Quantity = ci.Quantity,
                          Price = ci.Price,
                          ServiceName = s.Name,
                          ServicePrice = s.Price
                      })
                .ToListAsync(cancellationToken);

            return items;
        }
    }


}
