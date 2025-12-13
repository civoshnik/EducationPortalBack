using Shared.RabbitMQ.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Events.Auth
{
    public record UserRegisteredEvent : IIntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string ConfirmToken { get; init; }
        public DateTime ExpiresAt { get; init; }
        public string ConfirmUrl { get; init; }
    }
}
