using Core.Global.Enums;

namespace Core.Api.TenantSwitch.Models.Entities.Tickets
{

    public class TicketStatus
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public TicketStatusKeys Key { get; set; }

        public bool IsDeleted { get; set; }

    }

}