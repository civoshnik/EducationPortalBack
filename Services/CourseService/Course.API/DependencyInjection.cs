using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Application.Interfaces;
using Shared.Infrastructure;
using Course.Application.Queries.GetCourseList;

namespace Course.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetCourseListQuery).Assembly));

            services.AddDbContext<EducationPortalDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
