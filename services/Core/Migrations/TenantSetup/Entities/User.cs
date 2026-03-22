namespace Core.Migrations.TenantSetup.Entities
{

    internal class User
    {

        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }
    
    }

}