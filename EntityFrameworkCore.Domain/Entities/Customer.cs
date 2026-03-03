using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.Entities
{
    public class Customer : Person
    {
        public CustomerType Type { get; set; }
    }

    public enum CustomerType
    {
        Individual,
        Company
    }
}
