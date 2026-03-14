namespace Core.Api.TenantSwitch.Models.Entities.Resources
{

    public class Resource
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }
    
    }

}