using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BMPBackend.Data_Access
{
    public class BMPDbContextFactory : IDesignTimeDbContextFactory<BMPDbContext>
    {
        public BMPDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var sqlServerDefault = configuration["DatabaseProviders:Local:IsDefault"];
            if (sqlServerDefault?.ToUpper() != "TRUE")
            {
                throw new Exception("No default database provider is set!");
            }

            var connectionString = configuration["DatabaseProviders:Local:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Database connection string is missing!");
            }

            var optionsBuilder = new DbContextOptionsBuilder<BMPDbContext>();
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.EnableRetryOnFailure();
                options.CommandTimeout(300);
            });

            return new BMPDbContext(optionsBuilder.Options);
        }
    }
}
