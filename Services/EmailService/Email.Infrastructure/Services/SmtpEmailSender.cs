using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            _client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true
            };
        }

        public async Task SendEmailAsync(EmailMessage message, CancellationToken ct = default)
        {
            var mail = new MailMessage(_from, message.To, message.Subject, message.Body);
            await _client.SendMailAsync(mail);
        }
    }
}
