using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.DbContexts;
using Core.Api.TenantSwitch.Services.Interfaces;

namespace Core.Api.TenantSwitch.Services
{

    public class TenantResolver : ITenantResolver
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDbContextFactory<AdminDbContext> _contextFactory;

        public TenantResolver(IHttpContextAccessor httpContextAccessor, IDbContextFactory<AdminDbContext> contextFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _contextFactory = contextFactory;
        }

        public string GetConnectionString()
        {
            if (_httpContextAccessor?.HttpContext == null || _contextFactory == null)
                return string.Empty;

            var hostKey = $"{GetFirstSubdomain()}-tenant";
            using var context = _contextFactory.CreateDbContext();
            var tenant = context.Tenants.AsNoTracking().FirstOrDefault(t => t.Key == hostKey);
            if (tenant != null)
                return tenant.ConnectionString;
                
            return string.Empty;
        }

        private string GetFirstSubdomain()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var host = httpContext.Request.Host.Host;

            var parts = host.Split('.');

            return parts.Length > 1 ? parts[0] : string.Empty;
        }

    }

}