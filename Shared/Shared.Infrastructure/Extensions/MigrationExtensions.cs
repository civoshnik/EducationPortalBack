using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static IServiceCollection ApplyMigrations<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TContext>();
            db.Database.Migrate();
            return services;
        }
    }
}
