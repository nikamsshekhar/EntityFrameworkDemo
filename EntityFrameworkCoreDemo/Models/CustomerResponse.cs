using EntityFrameworkCore.Domain.Entities;

namespace EntityFrameworkCoreDemo.Models
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public CustomerType Type { get; set; }
        public Address Address { get; set; }
    }
}
