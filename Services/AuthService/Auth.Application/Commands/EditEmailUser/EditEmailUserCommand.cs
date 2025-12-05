
using MediatR;

namespace Auth.Application.Commands.EditEmailUser
{
    public record EditEmailUserCommand : IRequest
    {
        public Guid UserID { get; set; }
        public string Email { get; set; }
    }
}
