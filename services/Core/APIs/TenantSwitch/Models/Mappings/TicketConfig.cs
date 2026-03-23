using Mapster;

using TicketRequest = Core.Api.TenantSwitch.Models.Requests.Tickets.Ticket;
using TicketEntity = Core.Api.TenantSwitch.Models.Entities.Tickets.Ticket;

namespace Core.Api.TenantSwitch.Models.Mappings
{

    public class TicketConfig : IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TicketRequest, TicketEntity>()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.Description, src => src.Description)
                .Ignore(dest => dest.AssignedToId)
                .Ignore(dest => dest.StatusId)
                .Ignore(dest => dest.CreatedDate)
                .Map(dest => dest.CreatedById, src => src.UserId)
                .Ignore(dest => dest.UpdatedDate)
                .Ignore(dest => dest.UpdatedById)
                .Ignore(dest => dest.IsDeleted);
        }
        
    }

}