namespace Core.Migrations.TenantSetup.Entities.Tickets
{

    internal class Ticket
    {

        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public Guid? AssignedToId { get; set; }
        public Resource AssignedTo { get; set; }

        public Guid StatusId { get; set; }
        public TicketStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        public User UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

    }

}