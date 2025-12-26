using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Commands.SetAdmin
{
    public record SetAdminCommand(Guid UserId) : IRequest;
}
