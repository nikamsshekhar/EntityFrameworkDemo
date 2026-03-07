using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository.Configurations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class CustomerConfigurationTests
    {
        [Fact]
        public void CustomerConfiguration_ShouldConfigureCustomerEntity()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var configuration = new CustomerConfiguration();

            // Act
            configuration.Configure(modelBuilder.Entity<Customer>());

            // Assert
            var entity = modelBuilder.Model.FindEntityType(typeof(Customer));
            entity.Should().NotBeNull();

            // Check key
            var key = entity.FindPrimaryKey();
            key.Should().NotBeNull();
            key.Properties.Should().Contain(p => p.Name == "Id");

            // Check required properties
            var firstNameProperty = entity.FindProperty("FirstName");
            firstNameProperty.Should().NotBeNull();
            firstNameProperty.IsNullable.Should().BeFalse();

            var lastNameProperty = entity.FindProperty("LastName");
            lastNameProperty.Should().NotBeNull();
            lastNameProperty.IsNullable.Should().BeFalse();

            var typeProperty = entity.FindProperty("Type");
            typeProperty.Should().NotBeNull();
            typeProperty.IsNullable.Should().BeFalse();

            var createdDateProperty = entity.FindProperty("CreatedDate");
            createdDateProperty.Should().NotBeNull();
            createdDateProperty.IsNullable.Should().BeFalse();

            // Check relationships
            var organizationNavigation = entity.FindNavigation("Organization");
            organizationNavigation.Should().NotBeNull();

            var addressNavigation = entity.FindNavigation("Address");
            addressNavigation.Should().NotBeNull();
        }
    }
}