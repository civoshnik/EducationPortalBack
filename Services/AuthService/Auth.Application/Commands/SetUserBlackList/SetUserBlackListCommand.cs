using MediatR;

namespace Auth.Application.Commands.SetUserBlackList
{
    public record SetUserBlackListCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
