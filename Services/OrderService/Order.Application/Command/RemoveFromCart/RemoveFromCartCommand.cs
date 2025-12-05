using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Command.RemoveFromCart
{
    public record RemoveFromCartCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
