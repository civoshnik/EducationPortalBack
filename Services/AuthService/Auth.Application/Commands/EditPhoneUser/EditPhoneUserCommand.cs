using MediatR;

namespace Auth.Application.Commands.EditPhoneUser
{
    public record EditPhoneUserCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string Phone { get; set; }
    }
}
