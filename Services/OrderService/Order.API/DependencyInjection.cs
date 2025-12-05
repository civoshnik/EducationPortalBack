using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using Shared.Infrastructure;
using MediatR;
using Order.Application.Queries;

namespace Order.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrderApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetPaginatedServicesQuery).Assembly));

            services.AddDbContext<EducationPortalDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
