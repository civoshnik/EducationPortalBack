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
    public class GetUserCartQueryHandler : IRequestHandler<GetUserCartQuery, List<CartItemEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserCartQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CartItemEntity>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.CartItems
                .Where(ci => ci.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return items;
        }
    }

}
