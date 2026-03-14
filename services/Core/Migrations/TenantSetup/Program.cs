using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Core.Migrations.TenantSetup.DbContexts;

namespace Core.Migrations.TenantSetup
{
    internal class Program
    {

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
                var tenants = dbContext.Tenants.AsNoTracking().ToList();
                foreach (var tenant in tenants)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
                    optionsBuilder.UseSqlServer(tenant.ConnectionString);
                    using (var tenantDbContext = new TenantDbContext(optionsBuilder.Options))
                    {
                        tenantDbContext.Database.Migrate();
                    }
                }
            }

        }

    }
}