using Shared.RabbitMQ.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Events.Auth
{
    public class UserRegisteredEvent : IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ConfirmToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string ConfirmUrl { get; set; }
    }
}
