using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Auth.Application.Queries.GetUser;

namespace Auth.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthApi(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetUserQuery).Assembly));

            return services;
        }
    }
}
