using MediatR;
using Order.Domain.Models;

namespace Order.Application.Queries
{
    public record GetPaginatedServicesQuery(int Page, int PageSize) : IRequest<List<ServiceEntity>>
    {

    }
}
