using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;


using Core.Api.TenantSwitch.DbContexts;
using Core.Api.TenantSwitch.Services;
using Core.Api.TenantSwitch.Services.Interfaces;

namespace Core.Api.TenantSwitch
{

    internal class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

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

            builder.Services.AddSingleton<ITenantResolver, TenantResolver>();

            // Add HttpContextAccessor is always Singleton
            builder.Services.AddHttpContextAccessor();

            using var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseCors();

            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");
            app.Run();
        }

    }

}