using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Core.Global;
using Core.Api.TenantSwitch.DbContexts;
using Core.Api.TenantSwitch.DbContexts.Interfaces;
using Core.Api.TenantSwitch.Services.Interfaces;

namespace Core.Api.TenantSwitch
{
    public static class DependencySetup
    {
        public static void AddCors(this IServiceCollection services, bool isDevelopment)
        {

            if (isDevelopment)
            {
                // Get CORS settings from Aspire-provided environment variable FRONTEND_ORIGIN
                var frontendOrigin = Environment.GetEnvironmentVariable("FRONTEND_ORIGIN");
                if (string.IsNullOrEmpty(frontendOrigin))
                    frontendOrigin = Constant.API.GetDefaultFrontendOrigin;
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        policy =>
                        {
                            policy.WithOrigins(frontendOrigin)
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        });
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        policy =>
                        {
                            policy.SetIsOriginAllowed(origin =>
                            {
                                if (string.IsNullOrEmpty(origin))
                                    return false;
                                try
                                {
                                    var uri = new Uri(origin);

                                    // http or https only
                                    if (uri.Scheme != "http" && uri.Scheme != "https")
                                        return false;

                                    var host = uri.Host.ToLowerInvariant();
                                    // For domains and subdomains
                                    if (host == Constant.API.FrontendOriginDomain || host.EndsWith($".{Constant.API.FrontendOriginDomain}"))
                                        return true;

                                    return false;
                                }
                                catch
                                {
                                    return false;
                                }
                            })
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        });
                });
            }
        }
        
        public static IServiceCollection AddTenantDbContext(this IServiceCollection services)
        {
            services.AddScoped<ITenantDbContext, TenantDbContext>(provider =>
            {
                var tenantResolver = provider.GetRequiredService<ITenantResolver>();
                var connectionString = tenantResolver.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.ConfigureWarnings(warnings =>
                {
                    warnings.Ignore(RelationalEventId.BoolWithDefaultWarning);
                });
                return new TenantDbContext(optionsBuilder.Options);
            });

            return services;
        }
    }
}