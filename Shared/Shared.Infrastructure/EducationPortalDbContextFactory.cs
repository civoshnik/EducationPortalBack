using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Shared.Infrastructure
{
    public class EducationPortalDbContextFactory : IDesignTimeDbContextFactory<EducationPortalDbContext>
    {
        public EducationPortalDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EducationPortalDbContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));

            return new EducationPortalDbContext(optionsBuilder.Options);
        }
    }
}
