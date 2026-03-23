using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Entities.Resources;
using Core.Api.TenantSwitch.Models.Entities.Tickets;

namespace Core.Api.TenantSwitch.DbContexts.Interfaces
{

    public interface ITenantDbContext
    {

        DbSet<Resource> Resources { get; }
        
        DbSet<Ticket> Tickets { get; }
        DbSet<TicketStatus> TicketStatuses { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }

}