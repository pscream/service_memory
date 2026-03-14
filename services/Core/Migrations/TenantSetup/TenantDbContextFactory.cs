using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Core.Migrations.TenantSetup.DbContexts;

namespace Core.Migrations.TenantSetup
{

    internal class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();

            // When this factory is used to produce migrations, the connection string doesn't matter and is for design-time only
            optionsBuilder.UseSqlServer("Server=localhost;Database=DesignTimeDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true");

            return new TenantDbContext(optionsBuilder.Options);
        }
    }

}