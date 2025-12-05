using MediatR;

namespace Auth.Application.Commands.CancelUSerBlackList
{
    public record CancelUserBlackListQuery : IRequest
    {
        public Guid UserId { get; set; }
    }
}
