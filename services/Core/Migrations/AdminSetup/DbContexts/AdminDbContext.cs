using Microsoft.EntityFrameworkCore;

using Core.Migrations.AdminSetup.Entities;
using Core.Migrations.AdminSetup.DbContexts.Configs;

namespace Core.Migrations.AdminSetup.DbContexts
{

    internal class AdminDbContext : DbContext
    {

        public DbSet<Tenant> Tenants { get; set; }

        public AdminDbContext(DbContextOptions<AdminDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new TenantConfiguration());

        }

    }

}