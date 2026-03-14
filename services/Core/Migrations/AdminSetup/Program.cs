using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Core.Migrations.AdminSetup.DbContexts;

namespace Core.Migrations.AdminSetup
{
    internal class Program
    {

        // static async Task Main(string[] args)

        static void Main(string[] args)
        {

            var builder = Host.CreateApplicationBuilder(args);

            builder.AddServiceDefaults();

            // Add DbContext with connection string from Aspire
            builder.AddSqlServerDbContext<AdminDbContext>(Global.Constant.Admin.DatabaseTag);

            var host = builder.Build();

            // Run migrations
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AdminDbContext>();
                dbContext.Database.Migrate();
                var tenants = dbContext.Tenants.AsTracking().ToList();
                foreach (var tenant in tenants)
                {
                    tenant.ConnectionString = builder.Configuration.GetConnectionString(tenant.ConnectionString);
                }
                dbContext.SaveChanges();
            }

        }
    }
}