using EntityFrameworkCore.Domain.Entities;

namespace EntityFrameworkCoreDemo.Models
{
    public class EmployeeResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Title { get; set; }
        public Role Role { get; set; }

        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }

        public Address Address { get; set; }
    }
}
