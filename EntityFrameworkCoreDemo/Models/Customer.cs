using EntityFrameworkCore.Domain.Entities;

namespace EntityFrameworkCoreDemo.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerType Type { get; set; }

        public int OrganizationId { get; set; }
        public int? AddressId { get; set; }
    }
}
