using System.Net;
using System.Net.Mail;
using Email.Domain;
using Shared.Application.Interfaces;

namespace Email.Infrastructure.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        private readonly string _from;

        public SmtpEmailSender(string host, int port, string user, string password)
        {
            _from = user;
            _client = new SmtpClient(host, 587)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Timeout = 5000
            };
        }

        public async Task SendEmailAsync(EmailMessage message, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(message.To))
                throw new ArgumentException("Поле 'To' не может быть пустым", nameof(message.To));

            try
            {
                var mail = new MailMessage(_from, message.To, message.Subject, message.Body)
                {
                    IsBodyHtml = false
                };

                Console.WriteLine($"📧 Отправка письма на {message.To}...");
                await _client.SendMailAsync(mail, ct);
                Console.WriteLine($"✅ Письмо успешно отправлено на {message.To}");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"❌ Ошибка SMTP: {ex.StatusCode} - {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"🔍 Внутренняя ошибка: {ex.InnerException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка отправки письма: {ex.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine("📨 Завершение обработки SMTP, ack можно отправлять");
            }
        }
    }
}