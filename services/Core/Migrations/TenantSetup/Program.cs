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
                        var tenantName = tenant.Name;

                        // Generate tenant prefix from tenant name
                        var words = tenantName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (words.Length == 0)
                            continue; // Skip if tenant name is empty

                        var prefix = words.Length > 1 ? $"{words[0][0]}{words[1][0]}" : $"{words[0][0]}";

                        // Update ticket codes with tenant prefix
                        var tickets = tenantDbContext.Tickets.ToList();
                        foreach (var ticket in tickets)
                            ticket.Code = $"{prefix}-{ticket.Code}";

                        tenantDbContext.SaveChanges();
                    }
                }
            }

        }

    }
}