namespace Core.Api.TenantSwitch.Models.Entities.Tenants
{

    public class Tenant
    {

        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public bool IsDeleted { get; set; }

    }

}