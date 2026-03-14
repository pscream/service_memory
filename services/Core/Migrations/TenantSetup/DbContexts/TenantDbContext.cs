using Microsoft.EntityFrameworkCore;

using Core.Migrations.TenantSetup.Entities;
using Core.Migrations.TenantSetup.DbContexts.Configs;

namespace Core.Migrations.TenantSetup.DbContexts
{

    internal class TenantDbContext : DbContext
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