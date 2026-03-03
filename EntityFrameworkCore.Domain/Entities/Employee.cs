using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.Entities
{
    public class Employee : Person
    {
        public Role? Role { get; set; } // Employee/manager/boss

        //Self reference
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

        //Organization
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }

    public enum Role { 
        Employee,
        Manager,
        Boss
    }
}
