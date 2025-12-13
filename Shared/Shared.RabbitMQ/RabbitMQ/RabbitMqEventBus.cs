using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMQ.Abstractions;

namespace Shared.RabbitMQ.RabbitMQ
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly RabbitMqPersistentConnection _connection;
        private readonly string _exchangeName = "app.events";

        public RabbitMqEventBus(RabbitMqPersistentConnection connection)
        {
            _connection = connection;
        }

        public async Task Publish(IIntegrationEvent @event)
        {
            var channel = await _connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true);

            var eventName = @event.GetType().Name;
            var body = JsonSerializer.SerializeToUtf8Bytes(@event);

            var props = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(
                exchange: _exchangeName,
                routingKey: eventName,
                mandatory: false,
                basicProperties: props,
                body: body
            );
        }

        public async Task Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IIntegrationEvent
        {
            var channel = await _connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true);

            var queueName = typeof(TEvent).Name.ToLower();
            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(queueName, _exchangeName, typeof(TEvent).Name);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<TEvent>(json);
                if (message is not null)
                    await handler(message);

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);
        }
    }
}
