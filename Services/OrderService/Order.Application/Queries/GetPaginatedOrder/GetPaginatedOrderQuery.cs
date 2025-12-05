using MediatR;
using Order.Domain.Models;
using Shared.Application.Models;

namespace Order.Application.Queries.GetPaginatedOrder
{
    public record GetPaginatedOrderQuery(int Page, int PageSize) : IRequest<PaginatedResult<OrderEntity>>
    {
    }
}
