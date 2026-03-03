using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.Entities
{
    public class Organization : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string PAN { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
