using MediatR;

namespace Order.Application.Command.EditService
{
    public record EditServiceCommand : IRequest
    {
        public Guid ServiceId { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
    }
}
