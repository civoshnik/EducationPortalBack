using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Command.AddToCart
{
    public record AddToCartCommand(Guid UserId, Guid ServiceId, int Quantity = 1) : IRequest<Guid>
    {
    }
}
