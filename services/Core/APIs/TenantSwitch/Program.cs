using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Mappings;
using Core.Api.TenantSwitch.DbContexts;
using Core.Api.TenantSwitch.Repos.Interfaces;
using Core.Api.TenantSwitch.Repos.Tickets;
using Core.Api.TenantSwitch.Services;
using Core.Api.TenantSwitch.Services.Interfaces;

namespace Core.Api.TenantSwitch
{

    internal class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddServiceDefaults();

            // Add services to the container.
            builder.Services.AddControllers();

            // No need to add health checks here because AddServiceDefaults() already adds a default liveness check
            // builder.Services.AddHealthChecks()
            //     .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

            // CORS setup
            builder.Services.AddCors(builder.Environment.IsDevelopment());

            builder.Services.AddDbContextFactory<AdminDbContext>(options =>
            {
                // Keep it commented. At some point one may want to debug.
                //options.EnableDetailedErrors(false);
                //options.EnableSensitiveDataLogging(false);
                //options.ConfigureWarnings(warning => 
                //{
                //    warning.Ignore(CoreEventId.SaveChangesFailed, RelationalEventId.ConnectionError);
                //});
                options.UseSqlServer(builder.Configuration.GetConnectionString(Global.Constant.Admin.DatabaseTag),
                    options => options.EnableRetryOnFailure(
                                        maxRetryCount: 5,
                                        maxRetryDelay: TimeSpan.FromSeconds(15),
                                        // We have to add these codes because they are not on the list for the current version of EF Core
                                        errorNumbersToAdd: new int[] { -1, -2, 4060 }));
            }, ServiceLifetime.Singleton);

            builder.Services.AddTenantDbContext();

            builder.Services.AddScoped<ITicketRepo, TicketRepo>();

            builder.Services.AddSingleton<ITenantResolver, TenantResolver>();

            // Add HttpContextAccessor is always Singleton
            builder.Services.AddHttpContextAccessor();

            builder.RegisterMapsterConfigs();

            using var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors();

            app.UseAuthorization();
            app.MapControllers();

            // No need for a separate health check endpoint because MapDefaultEndpoints() from ServiceDefaults already adds a default liveness check
            //app.MapHealthChecks("/health");
            app.MapDefaultEndpoints();
            app.Run();
        }

    }

}