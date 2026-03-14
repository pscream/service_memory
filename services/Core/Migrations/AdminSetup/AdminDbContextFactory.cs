using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Core.Migrations.AdminSetup.DbContexts;

namespace Core.Migrations.AdminSetup
{

    internal class AdminDbContextFactory : IDesignTimeDbContextFactory<AdminDbContext>
    {
        public AdminDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdminDbContext>();

            // When this factory is used to produce migrations, the connection string doesn't matter and is for design-time only
            optionsBuilder.UseSqlServer("Server=localhost;Database=DesignTimeDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true");

            return new AdminDbContext(optionsBuilder.Options);
        }
    }

}