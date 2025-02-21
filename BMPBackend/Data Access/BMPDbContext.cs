using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using BMPBackend.Modules.CustomerModule.Model;
using BMPBackend.Modules.FinanceModule.Model;
using BMPBackend.Modules.UserModule.User;

namespace BMPBackend.Data_Access
{
    public class BMPDbContext : DbContext
    {
        public BMPDbContext(DbContextOptions<BMPDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = Assembly.GetAssembly(GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
