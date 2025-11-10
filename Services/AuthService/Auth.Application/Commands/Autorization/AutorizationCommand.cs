using MediatR;
using System.Diagnostics.Contracts;

namespace Auth.Application.Commands.Autorization
{
    public record AutorizationCommand : IRequest<AuthResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
