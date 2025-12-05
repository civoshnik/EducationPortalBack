using MediatR;
using Order.Domain.Models;

namespace Order.Application.Queries.GetUserOrders
{
    public record GetUserOrdersQuery : IRequest<List<OrderEntity>>
    {
        public Guid UserId { get; set; }
    }
}
