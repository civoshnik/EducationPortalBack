using MediatR;
using Order.Domain.Models;
using Shared.Application.Models;
using System;
using System.Collections.Generic;

namespace Order.Application.Queries.GetOrdersByService
{
    public record GetOrdersByServiceQuery : IRequest<List<OrderEntity>>
    {
        public Guid ServiceId { get; set; }
    }
}
