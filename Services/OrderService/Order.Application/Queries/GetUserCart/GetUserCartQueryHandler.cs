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
    //public class GetUserCartQueryHandler : IRequestHandler<GetUserCartQuery, CartEntity>
    //{
    //    private readonly IUnitOfWork _unitOfWork;

    //    public GetUserCartQueryHandler(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }

    //    public async Task<CartEntity> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
    //    {
    //        var cart = await _unitOfWork.Carts
    //            .Include(c => c.Items)
    //            .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

    //        if (cart == null)
    //        {
    //            cart = new CartEntity
    //            {
    //                CartId = Guid.NewGuid(),
    //                UserId = request.UserId,
    //                CreatedAt = DateTime.UtcNow,
    //                Items = new List<CartItemEntity>()
    //            };

    //            await _unitOfWork.Carts.AddAsync(cart, cancellationToken);
    //            await _unitOfWork.SaveChangesAsync(cancellationToken);
    //        }

    //        return cart;
    //    }
    //}
}
