using Core.Global.Enums;

namespace Core.Migrations.TenantSetup.Entities.Tickets
{

    internal class TicketStatus
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public TicketStatusKeys Key { get; set; }

        public bool IsDeleted { get; set; }

    }

}