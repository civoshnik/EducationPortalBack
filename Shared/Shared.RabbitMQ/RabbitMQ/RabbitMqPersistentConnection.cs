using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.RabbitMQ
{
    public class RabbitMqPersistentConnection
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqPersistentConnection(RabbitMqOptions options)
        {
            _factory = new ConnectionFactory
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                Port = options.Port,

                HandshakeContinuationTimeout = TimeSpan.FromSeconds(60),

                RequestedConnectionTimeout = TimeSpan.FromSeconds(60)
            };
        }

        public async Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default)
        {
            IConnection connection = await _factory.CreateConnectionAsync(cancellationToken);
            return await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        }
    }
}