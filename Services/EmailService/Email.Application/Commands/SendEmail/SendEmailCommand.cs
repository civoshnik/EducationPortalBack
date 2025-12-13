using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Application.Commands.SendEmail
{
    public record SendEmailCommand(string To, string Subject, string Body) : IRequest<bool>;
}
