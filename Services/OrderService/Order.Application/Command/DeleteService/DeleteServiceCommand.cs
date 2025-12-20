using MediatR;

namespace Order.Application.Command.DeleteService
{
    public record DeleteServiceCommand : IRequest
    {
        public Guid ServiceId { get; set; }
    }
}
