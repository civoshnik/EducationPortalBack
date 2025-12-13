using Email.Domain;
using MediatR;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Application.Commands.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IEmailSender _sender;

        public SendEmailCommandHandler(IEmailSender sender)
        {
            _sender = sender;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken ct)
        {
            var message = new EmailMessage
            {
                To = request.To,
                Subject = request.Subject,
                Body = request.Body
            };

            await _sender.SendEmailAsync(message, ct);
            return true;
        }
    }
}
