using BMPBackend.Modules.FinanceModule.Model;
using BMPBackend.Modules.UserModule.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BMPBackend.Data_Access
{
    public class BMPDbContext : DbContext
    {
        public BMPDbContext()
        {

        }

        public BMPDbContext(DbContextOptions<DbContext> options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.GetAssembly(GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var sqlServerDefault = config.GetSection("DatabaseProviders")
                .GetSection("Local")["IsDefault"] ?? "";

            if (sqlServerDefault.ToUpper() == "TRUE")
            {
                var connString = config.GetSection("DatabaseProviders")
                    .GetSection("Local")["ConnectionString"];
                optionsBuilder.UseSqlServer(connString, option =>
                {
                    option.EnableRetryOnFailure();
                    option.CommandTimeout(300);
                });
            }

            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
