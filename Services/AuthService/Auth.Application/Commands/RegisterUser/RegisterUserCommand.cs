using MediatR;
using System.Diagnostics.Contracts;

namespace Auth.Application.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

    }
}
