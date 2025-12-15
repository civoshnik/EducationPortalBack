using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Abstractions
{
    public interface IEventBus
    {
        Task Publish(IIntegrationEvent @event);
        Task Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IIntegrationEvent;
        Task PurgeQueueAsync<TEvent>() where TEvent : IIntegrationEvent;
    }
}

