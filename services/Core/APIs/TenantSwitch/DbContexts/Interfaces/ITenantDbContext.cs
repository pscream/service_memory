using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Entities.Resources;

namespace Core.Api.TenantSwitch.DbContexts.Interfaces
{

    public interface ITenantDbContext
    {
        DbSet<Resource> Resources { get; }
    }

}