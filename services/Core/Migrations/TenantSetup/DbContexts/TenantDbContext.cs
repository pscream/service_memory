using Microsoft.EntityFrameworkCore;

using Core.Migrations.TenantSetup.Entities.Tickets;
using Core.Migrations.TenantSetup.DbContexts.Configs;
using Core.Migrations.TenantSetup.DbContexts.Configs.Tickets;

namespace Core.Migrations.TenantSetup.DbContexts
{

    internal class TenantDbContext : DbContext
    {

        public DbSet<Ticket> Tickets { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());

            #region Tickets

            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TicketStatusConfiguration());

            #endregion

        }

    }

}