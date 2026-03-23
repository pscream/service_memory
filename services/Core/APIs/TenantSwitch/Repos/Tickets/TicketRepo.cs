using Core.Global.Enums;
using Core.Api.TenantSwitch.Models.Entities.Tickets;
using Core.Api.TenantSwitch.DbContexts.Interfaces;
using Core.Api.TenantSwitch.Repos.Interfaces;

namespace Core.Api.TenantSwitch.Repos.Tickets
{

    internal class TicketRepo : ITicketRepo
    {

        private readonly ITenantDbContext _tenantContext;

        public TicketRepo(ITenantDbContext tenantContext)
{
            _tenantContext = tenantContext;
        }

        public async Task CreateAsync(Ticket ticket)
        {
            ticket.Id = Guid.NewGuid();
            ticket.StatusId = _tenantContext.TicketStatuses.FirstOrDefault(ts => ts.Key == TicketStatusKeys.New).Id;
            ticket.CreatedDate = DateTime.UtcNow;
            _tenantContext.Tickets.Add(ticket);
            await _tenantContext.SaveChangesAsync();   
        }

    }

}