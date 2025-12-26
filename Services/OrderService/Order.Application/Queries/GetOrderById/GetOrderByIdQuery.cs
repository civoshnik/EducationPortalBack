using MediatR;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderWithServicesDto>;
}
