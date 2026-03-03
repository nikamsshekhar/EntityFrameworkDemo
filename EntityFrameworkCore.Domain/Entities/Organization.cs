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
        public Address Address { get; set; }
        public int Phone { get; set; }
        public string PAN { get; set; }

        public List<Employee> Employees { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
