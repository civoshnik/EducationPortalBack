using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Command.CreateOrder
{
    public record CreateOrderCommand(Guid UserId, List<OrderItem> Services) : IRequest<Guid>
    {

    }

}
