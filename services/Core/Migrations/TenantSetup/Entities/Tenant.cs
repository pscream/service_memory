namespace Core.Migrations.TenantSetup.Entities
{

    internal class Tenant
    {

        public Guid Id { get; set; }

        public string ConnectionString { get; set; }

        public bool IsDeleted { get; set; }

    }

}