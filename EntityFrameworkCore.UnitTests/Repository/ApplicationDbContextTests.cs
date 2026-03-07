using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class ApplicationDbContextTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ApplicationDbContextTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public void ApplicationDbContext_ShouldBeInstantiated()
        {
            // Act
            using var context = new ApplicationDbContext(_options);

            // Assert
            context.Should().NotBeNull();
        }

        [Fact]
        public void ApplicationDbContext_ShouldHaveOrganizationsDbSet()
        {
            // Act
            using var context = new ApplicationDbContext(_options);

            // Assert
            context.Organizations.Should().NotBeNull();
            context.Organizations.Should().BeAssignableTo<DbSet<Organization>>();
        }

        [Fact]
        public void ApplicationDbContext_ShouldHaveEmployeesDbSet()
        {
            // Act
            using var context = new ApplicationDbContext(_options);

            // Assert
            context.Employees.Should().NotBeNull();
            context.Employees.Should().BeAssignableTo<DbSet<Employee>>();
        }

        [Fact]
        public void ApplicationDbContext_ShouldHaveCustomersDbSet()
        {
            // Act
            using var context = new ApplicationDbContext(_options);

            // Assert
            context.Customers.Should().NotBeNull();
            context.Customers.Should().BeAssignableTo<DbSet<Customer>>();
        }

        [Fact]
        public void ApplicationDbContext_ShouldHaveAddressesDbSet()
        {
            // Act
            using var context = new ApplicationDbContext(_options);

            // Assert
            context.Addresses.Should().NotBeNull();
            context.Addresses.Should().BeAssignableTo<DbSet<Address>>();
        }
    }
}