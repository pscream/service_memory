using Microsoft.AspNetCore.Mvc;

using Mapster;

using Core.Api.TenantSwitch.Repos.Interfaces;

using TicketRequest = Core.Api.TenantSwitch.Models.Requests.Tickets.Ticket;
using TicketEntity = Core.Api.TenantSwitch.Models.Entities.Tickets.Ticket;

namespace Core.Api.TenantSwitch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {

        private readonly ITicketRepo _ticketRepo;

        public TicketController(ITicketRepo ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }
        
        // POST
        [HttpPost]
        public async Task Post([FromBody] TicketRequest ticket)
        {
            var ticketEntity = ticket.Adapt<TicketEntity>();
            await _ticketRepo.CreateAsync(ticketEntity);            
        }

    }
}