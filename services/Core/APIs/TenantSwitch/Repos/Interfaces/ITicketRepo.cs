using Core.Api.TenantSwitch.Models.Entities.Tickets;

namespace Core.Api.TenantSwitch.Repos.Interfaces
{

    public interface ITicketRepo
    {
        Task CreateAsync(Ticket ticket);
    }

}
