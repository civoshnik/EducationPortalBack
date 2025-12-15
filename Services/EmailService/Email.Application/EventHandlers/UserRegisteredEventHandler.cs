using Email.Application.Commands.SendEmail;
using MediatR;
using Shared.RabbitMQ.Events.Auth;

namespace Email.Application.EventHandlers
{
    public class UserRegisteredEventHandler
    {
        private readonly IMediator _mediator;

        public UserRegisteredEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserRegisteredEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            if (string.IsNullOrWhiteSpace(@event.Email))
            {
                Console.WriteLine($"❌ Email пустой в событии: {System.Text.Json.JsonSerializer.Serialize(@event)}");
                throw new ArgumentException("Поле Email не может быть пустым");
            }

            Console.WriteLine($"✅ Получено событие для пользователя: {@event.Email} ({@event.FirstName} {@event.LastName})");

            var subject = "Подтверждение email";
            var body = $@"Здравствуйте, {@event.FirstName} {@event.LastName}!
Для подтверждения перейдите по ссылке:
{@event.ConfirmUrl}
Ссылка действительна до {@event.ExpiresAt:yyyy-MM-dd HH:mm} UTC.";

            try
            {
                // 👉 Вместо прямого вызова IEmailSender отправляем команду через MediatR
                var command = new SendEmailCommand(@event.Email, subject, body);
                var result = await _mediator.Send(command);

                if (result)
                    Console.WriteLine($"📧 Письмо успешно отправлено на {@event.Email}");
                else
                    Console.WriteLine($"❌ Не удалось отправить письмо на {@event.Email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка обработки события: {ex.Message}");
                throw;
            }
        }
    }
}
