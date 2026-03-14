namespace Core.Migrations.TenantSetup.Entities
{

    internal class Resource
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }
    
    }

}