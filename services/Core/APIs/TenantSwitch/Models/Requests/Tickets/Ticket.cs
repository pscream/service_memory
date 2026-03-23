namespace Core.Api.TenantSwitch.Models.Requests.Tickets
{

    public class Ticket
    {

        public string Code { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

    }

}