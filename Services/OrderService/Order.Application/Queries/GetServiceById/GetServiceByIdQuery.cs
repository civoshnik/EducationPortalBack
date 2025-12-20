using MediatR;
using Order.Domain.Models;

namespace Order.Application.Queries.GetServiceById
{
    public record GetServiceByIdQuery : IRequest<ServiceEntity>
    {
        public Guid ServiceId { get; set; }
    }
}
