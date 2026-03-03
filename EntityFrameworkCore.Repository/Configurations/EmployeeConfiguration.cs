using EntityFrameworkCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repository.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(_ => _.LastName).IsRequired().HasMaxLength(50);
            builder.Property(_ => _.Address).IsRequired();
        }
    }
}
