using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.Entities
{
    public class Employee : Person
    {
        public Role Role { get; set; } // Employee/manager/boss
        public Employee Manager { get; set; }
    }

    public enum Role { 
        Employee,
        Manager,
        Boss
    }
}
