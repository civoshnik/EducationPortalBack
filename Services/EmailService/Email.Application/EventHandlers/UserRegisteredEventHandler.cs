using Email.Domain;
using Shared.Application.Interfaces;
using Shared.RabbitMQ.Events.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Application.EventHandlers
{
    public class UserRegisteredEventHandler
    {
        private readonly IEmailSender _sender;

        public UserRegisteredEventHandler(IEmailSender sender)
        {
            _sender = sender;
        }

        public async Task Handle(UserRegisteredEvent @event)
        {
            var subject = "Подтверждение email";
            var body = $@"Здравствуйте, {@event.FirstName} {@event.LastName}!
Для подтверждения перейдите по ссылке:
{@event.ConfirmUrl}
Ссылка действительна до {@event.ExpiresAt:yyyy-MM-dd HH:mm} UTC.";

            await _sender.SendEmailAsync(new EmailMessage
            {
                To = @event.Email,
                Subject = subject,
                Body = body
            });
        }
    }
}
