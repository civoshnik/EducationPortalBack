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

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyProperties = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public RabbitMqEventBus(RabbitMqPersistentConnection connection)
        {
            _connection = connection;
        }

        public async Task Publish(IIntegrationEvent @event)
        {
            var channel = await _connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true);

            var eventName = @event.GetType().Name;
            var eventJson = JsonSerializer.Serialize(@event, @event.GetType(), _jsonOptions);
            var body = Encoding.UTF8.GetBytes(eventJson);

            Console.WriteLine($"Отправка события {eventName}: {eventJson}");

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

            var eventName = typeof(TEvent).Name;
            var queueName = $"{typeof(TEvent).Name.ToLower()}_queue";

            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(queueName, _exchangeName, eventName);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"Получено сообщение: {json}");

                try
                {
                    var message = JsonSerializer.Deserialize<TEvent>(json, _jsonOptions);
                    if (message is not null)
                        await handler(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки сообщения: {ex.Message}");
                }
                finally
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                }
            };

            await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);
            Console.WriteLine($"Подписан на событие {eventName} (очередь: {queueName})");
        }

        public async Task PurgeQueueAsync<TEvent>() where TEvent : IIntegrationEvent
        {
            var channel = await _connection.CreateChannelAsync();
            var queueName = $"{typeof(TEvent).Name.ToLower()}_queue";

            try
            {
                await channel.QueuePurgeAsync(queueName);
                Console.WriteLine($"Очередь {queueName} очищена");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось очистить очередь {queueName}: {ex.Message}");
            }
        }
    }
}
