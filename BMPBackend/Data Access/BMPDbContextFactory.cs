using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMPBackend.Data_Access
{
    public class BMPDbContextFactory
    {
        public class PracticeDbContextFactory : IDesignTimeDbContextFactory<BMPDbContext>
        {
            public BMPDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
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
                    optionsBuilder.UseSqlServer(connString);
                }

                return new BMPDbContext(optionsBuilder.Options);
            }
        }
    }
}
