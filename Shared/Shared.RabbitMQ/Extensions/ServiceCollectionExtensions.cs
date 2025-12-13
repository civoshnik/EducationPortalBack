using Microsoft.Extensions.DependencyInjection;
using Shared.RabbitMQ.Abstractions;
using Shared.RabbitMQ.RabbitMQ;
using Microsoft.Extensions.Configuration;


namespace Shared.RabbitMQ.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration config)
        {
            var options = config.GetSection("RabbitMQ").Get<RabbitMqOptions>()!;
            services.AddSingleton(options);
            services.AddSingleton<RabbitMqPersistentConnection>();
            services.AddSingleton<IEventBus, RabbitMqEventBus>();
            return services;
        }
    }
}
