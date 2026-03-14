using Core.Global;

namespace Core.Hosts.AppHost
{

    internal class Program
    {

        static async Task Main(string[] args)
        {

            var builder = DistributedApplication.CreateBuilder(args);

            var sqlServer = builder.AddSqlServer("sqlserver")
                                    .WithImageTag("2022-latest");

            var adminDatabase = sqlServer.AddDatabase(Constant.Admin.DatabaseTag);
            var deepBlueDatabase = sqlServer.AddDatabase(Constant.Tenant.DeepBlueDatabaseTag);
            var stormSharkDatabase = sqlServer.AddDatabase(Constant.Tenant.StormSharkDatabaseTag);

            var migrationAdmin = builder.AddProject<Projects.AdminSetup>("migration-admin")
                                    .WithReference(adminDatabase)
                                    .WithReference(deepBlueDatabase)
                                    .WithReference(stormSharkDatabase)
                                    .WaitFor(adminDatabase);

            var deepBlueMigrationTenant = builder.AddProject<Projects.TenantSetup>("migration-tenant")
                                    .WithReference(adminDatabase)
                                    .WithReference(deepBlueDatabase)
                                    .WithReference(stormSharkDatabase)
                                    .WaitForCompletion(migrationAdmin)
                                    .WaitFor(deepBlueDatabase)
                                    .WaitFor(stormSharkDatabase);

            var coreApi = builder.AddProject<Projects.TenantSwitch>("api-core",
                                    configure: static project =>
                                    {
                                        project.ExcludeLaunchProfile = false; // Get port from launchSettings.json
                                        project.ExcludeKestrelEndpoints = true;
                                    })
                                    .WithEnvironment("ASPNETCORE_ENVIRONMENT", builder.Environment.EnvironmentName)
                                    //.WithHttpEndpoint() // commented out to use endpoints from launchSettings.json
                                    .WithHttpHealthCheck("/health")
                                    .WithDatabase(adminDatabase, deepBlueDatabase, stormSharkDatabase)
                                    .WaitForCompletion(deepBlueMigrationTenant);

            var reactApp = builder.AddNpmApp("react-frontend", "../../../../frontends/app", "aspire")
                                   .WithHttpEndpoint(port: Constant.API.DefaultFrontendOriginPort, env: "VITE_PORT")
                                   .WithEnvironment("VITE_CORE_API_BASE", coreApi.GetEndpoint("http"))
                                   .WithHttpHealthCheck("/")
                                   .WithReference(coreApi)
                                   .WaitFor(coreApi);

            coreApi.WithEnvironment("FRONTEND_ORIGIN", reactApp.GetEndpoint("http"));

            var app = builder.Build();

            app.Run();

        }

    }

}