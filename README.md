# service_memory
## The PoC of the tenant service with the database dictionary caching

### Commands used to scaffold the solution

List all scaffolding template created by Microsoft and install Aspire templates if needed
```powershell
PS C:\service_memory> dotnet new search --author Microsoft
PS C:\service_memory> dotnet new install Aspire.ProjectTemplates
```

Create a solution, its projects, and '.gitignore'
```powershell
PS C:\service_memory> dotnet new solution --name Core --output services/Core --format slnx
PS C:\service_memory> dotnet new create gitignore --output services/Core

PS C:\service_memory> dotnet new create aspire-apphost --no-restore -o services/Core/Hosts/AppHost
PS C:\service_memory> dotnet solution services/Core/Core.slnx add services/Core/Hosts/AppHost/AppHost.csproj

PS C:\service_memory> dotnet new create aspire-servicedefaults --no-restore -o services/Core/Hosts/ServiceDefaults
PS C:\service_memory> dotnet solution services/Core/Core.slnx add services/Core/Hosts/ServiceDefaults/ServiceDefaults.csproj

PS C:\service_memory> dotnet new create web --no-restore -o services/Core/APIs/TenantEmpty.API
PS C:\service_memory> dotnet new create webapi --use-controllers  --no-openapi --no-https --no-restore -o services/Core/APIs/TenantSwitch.API
```

Add Admin setup migration project
```powershell
PS C:\service_memory\services> dotnet new classlib -n AdminSetup -o "./Core/Migrations/AdminSetup" -f net10.0
PS C:\service_memory\services> cd .\Core\Migrations\AdminSetup\
PS C:\service_memory\services\Core\Migrations\AdminSetup> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
PS C:\service_memory\services\Core\Migrations\AdminSetup> dotnet add package Microsoft.EntityFrameworkCore.Design
PS C:\service_memory\services\Core\Migrations\AdminSetup> dotnet add package Aspire.Microsoft.EntityFrameworkCore.SqlServer
PS C:\service_memory\services\Core\Migrations\AdminSetup> dotnet add package Aspire.Hosting.SqlServer
```

Add Tenant setup migration project
```powershell
PS C:\service_memory\services> dotnet new classlib -n TenantSetup -o "./Core/Migrations/TenantSetup" -f net10.0
PS C:\service_memory\services> cd .\Core\Migrations\TenantSetup\
PS C:\service_memory\services\Core\Migrations\TenantSetup> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
PS C:\service_memory\services\Core\Migrations\TenantSetup> dotnet add package Microsoft.EntityFrameworkCore.Design
PS C:\service_memory\services\Core\Migrations\TenantSetup> dotnet add package Aspire.Microsoft.EntityFrameworkCore.SqlServer
PS C:\service_memory\services\Core\Migrations\TenantSetup> dotnet add package Aspire.Hosting.SqlServer
```

Install EF tools locally for the projects (AdminSetup and TenantSetup)
```powershell
PS C:\service_memory\services\Core\Migrations\AdminSetup> dotnet new tool-manifest
PS C:\service_memory\services\Core\Migrations\AdminSetup> dotnet tool install dotnet-ef
PS C:\service_memory\services\Core\Migrations\AdminSetup> dotnet ef migrations add Create_Tenants
PS C:\service_memory\services\Core\Migrations\TenantSetup> dotnet ef migrations add Init_Data --context TenantDbContext

PS C:\service_memory\services\Core\Migrations\TenantSetup> dotnet add package Microsoft.Extensions.Hosting
```

Create a controller
```powershell
PS C:\service_memory\services\Core\APIs\TenantSwitch> dotnet new create apicontroller --name "ResourceController" --output Controllers --namespace "Core.Api.TenantSwitch.Controllers" --actions
```

### Data Migrations

#### Admin database
Since the solution implies a multi‑tenant setup, a mechanism for switching between tenant contexts is required. Tenant information is stored in the admin database (*Constant.Admin.DatabaseTag*). Among other data, the connection strings to the tenant databases are also stored there.
To seed the admin database, a dedicated project called 'AdminSetup' is used. This project contains a design‑time DbContext factory (*AdminDbContextFactory*). If you need to run migrations manually, the connection string for the admin database must be properly configured in that factory:
```csharp
optionsBuilder.UseSqlServer("Server=localhost;Database=DesignTimeDb;User Id=sa;Password=YourStrongPassword;TrustServerCertificate=true");
``` 

When the 'AdminSetup' project is executed by Aspire, it creates a scope, migrates the database using the connection string provided by Aspire, and updates the actual tenant connection strings, which are also supplied by Aspire:
```csharp
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AdminDbContext>();
    dbContext.Database.Migrate();
    var tenants = dbContext.Tenants.AsTracking().ToList();
    foreach (var tenant in tenants)
    {
        tenant.ConnectionString = builder.Configuration.GetConnectionString(tenant.ConnectionString);
    }
    dbContext.SaveChanges();
}
``` 

#### Tenant databases
Tenant databases contain tenant‑specific data. The 'TenantSetup' project is executed by Aspire to seed these databases. Currently, all tenant databases are populated with the same initial set of records.
```csharp
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AdminDbContext>();
    var tenants = dbContext.Tenants.AsNoTracking().ToList();
    foreach (var tenant in tenants)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseSqlServer(tenant.ConnectionString);
        using (var tenantDbContext = new TenantDbContext(optionsBuilder.Options))
        {
            tenantDbContext.Database.Migrate();
        }
    }
}
```
In order to simulate the workload, the 'Ticket' table is created. The table is populated with 100 records. To distinguish data from different tenants the ticket code is prefixed with the code of tenant: 'Deep Blue Tenant' as 'DB-' -> 'DB-00000001', 'Storm Shark Tenant' as 'SS-' -> 'SS-00000001'.
```csharp
var prefix = words.Length > 1 ? $"{words[0][0]}{words[1][0]}" : $"{words[0][0]}";
...
foreach (var ticket in tickets)
    ticket.Code = $"{prefix}-{ticket.Code}";
```

### Cross-Origin Resource Sharing (CORS) policy in the Web APIs
Generally, the mechanism is the following: 
- the 'access-control-request-' request headers sent by the frontend with the the OPTIONS method request;
- the backend replies with the response headers;
```
access-control-allow-credentials    true
access-control-allow-headers        coreapi-tenant-key
access-control-allow-methods        GET
access-control-allow-origin         http://localhost:3030
```
- if the backend didn't reply with the 'access-control-allow-*' headers, the browser blocks the next GET fetch, and write an error to the console *Access to fetch at 'http://localhost:4001/api/resource' from origin 'http://localhost:62387' has been blocked by CORS policy: Response to preflight request doesn't pass access control check: No 'Access-Control-Allow-Origin' header is present on the requested resource*.

Inside the startup code the logic is following
```csharp
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
```
If a web api is started by Aspire, the 'FRONTEND_ORIGIN' environmental variable is used to setup the origin of the frontend. The 'FRONTEND_ORIGIN' environmental variable is set in the AppHost project. It's inferred from the 'reactApp' Aspire resource.
```csharp
coreApi.WithEnvironment("FRONTEND_ORIGIN", reactApp.GetEndpoint("http"));
```
If the frontend is started manually, the default configuration is used - the 'Constant.API.GetDefaultFrontendOrigin' constant.
In a cluster (production) environment, the origin is inferred from the real domain (the 'Constant.API.FrontendOriginDomain' constant), and all subdomains and ports are accepted as valid.

### Debug in Aspire from VS Code
#### Option 1
- Install Aspire VS Code extension to add Aspire CLI;
- Go to 'Run & Debug' section, select 'Aspire: Launch AppHost', and click 'Run'
- It will hit breakpoints when resources are started by Aspire

#### Option 2
Go to 'Run & Debug' section, select 'Attach to .NET Process', and look for the exact name of the assembly without the 'dotnet' command in front of it.
For example, DON'T pick 'dotnet run --project ..../Core/APIs/TenantSwitch/TenantSwitch.csproj --no-build --configuration Debug --no-launch-profile', pick '..../Core/APIs/TenantSwitch/bin/Debug/net10.0/TenantSwitch.exe'. If you see the message 'No symbols have been loaded for this document' when you try to set a breakpoint in the assembly’s source code, it means the wrong .NET process was attached.