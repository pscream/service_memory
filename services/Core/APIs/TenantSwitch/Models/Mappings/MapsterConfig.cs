using System.Reflection;

using Mapster;

namespace Core.Api.TenantSwitch.Models.Mappings
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfigs(this WebApplicationBuilder builder)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            TypeAdapterConfig.GlobalSettings.Compile(); // validate all mappings eagerly
        }
    }
}