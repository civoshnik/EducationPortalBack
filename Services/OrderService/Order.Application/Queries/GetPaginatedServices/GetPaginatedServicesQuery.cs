using MediatR;
using Order.Domain.Models;
using Shared.Application.Models;

namespace Order.Application.Queries
{
    public record GetPaginatedServicesQuery(int Page, int PageSize) : IRequest<PaginatedResult<ServiceEntity>>;
}
