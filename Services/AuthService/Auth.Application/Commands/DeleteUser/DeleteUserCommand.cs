using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
