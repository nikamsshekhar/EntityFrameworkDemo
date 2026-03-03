using EntityFrameworkCore.Domain.Entities;

namespace EntityFrameworkCoreDemo.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public Role Role { get; set; }

        public int OrganizationId { get; set; }
        public int? ManagerId { get; set; }

        public int? AddressId { get; set; }
    }
}
