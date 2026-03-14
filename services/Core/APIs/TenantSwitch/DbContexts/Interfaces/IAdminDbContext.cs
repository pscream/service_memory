using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Entities.Tenants;

namespace Core.Api.TenantSwitch.DbContexts.Interfaces
{

    public interface IAdminDbContext
    {
        DbSet<Tenant> Tenants { get; }
    }

}