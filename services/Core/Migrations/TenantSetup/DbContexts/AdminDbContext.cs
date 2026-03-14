using Microsoft.EntityFrameworkCore;

using Core.Migrations.TenantSetup.Entities;
using Core.Migrations.TenantSetup.DbContexts.Configs;

namespace Core.Migrations.TenantSetup.DbContexts
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