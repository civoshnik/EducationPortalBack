using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(Guid UserId, string Token) : IRequest<bool>
    {

    }
}

