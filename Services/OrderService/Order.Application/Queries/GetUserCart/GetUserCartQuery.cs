using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Domain.Models;

namespace Order.Application.Queries.GetUserCart
{
    public record GetUserCartQuery : IRequest<List<CartItemDto>>
    {
        public Guid UserId { get; set; }
    }
}
