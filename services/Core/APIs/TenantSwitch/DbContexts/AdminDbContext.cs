using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Entities.Tenants;
using Core.Api.TenantSwitch.DbContexts.Interfaces;
using Core.Api.TenantSwitch.DbContexts.Configs;

namespace Core.Api.TenantSwitch.DbContexts
{

    public class AdminDbContext : DbContext, IAdminDbContext
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