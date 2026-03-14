using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Entities.Resources;
using Core.Api.TenantSwitch.DbContexts.Interfaces;
using Core.Api.TenantSwitch.DbContexts.Configs;

namespace Core.Api.TenantSwitch.DbContexts
{

    internal class TenantDbContext : DbContext, ITenantDbContext
    {

        public DbSet<Resource> Resources { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ResourceConfiguration());

        }

    }

}