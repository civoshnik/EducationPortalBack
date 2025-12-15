using MediatR;

namespace Auth.Application.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(Guid UserId, string Token) : IRequest<bool>;
}
