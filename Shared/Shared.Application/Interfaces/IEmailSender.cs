using Email.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken = default);
    }
}
