using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Entities.Resources;
using Core.Api.TenantSwitch.Models.Entities.Tickets;
using Core.Api.TenantSwitch.DbContexts.Interfaces;
using Core.Api.TenantSwitch.DbContexts.Configs;
using Core.Api.TenantSwitch.DbContexts.Configs.Tickets;

namespace Core.Api.TenantSwitch.DbContexts
{

    internal class TenantDbContext : DbContext, ITenantDbContext
    {

        public DbSet<Resource> Resources { get; set; }
        
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ResourceConfiguration());

            #region Tickets

            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TicketStatusConfiguration());
            
            #endregion

        }

    }

}