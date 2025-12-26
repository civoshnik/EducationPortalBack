using MediatR;

namespace Course.Application.Commands.DeleteTest
{
    public record DeleteTestCommand : IRequest
    {
        public Guid TestId { get; set; }
    }
}
