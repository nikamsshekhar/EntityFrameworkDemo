using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository.Configurations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class EmployeeConfigurationTests
    {
        [Fact]
        public void EmployeeConfiguration_ShouldConfigureEmployeeEntity()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var configuration = new EmployeeConfiguration();

            // Act
            configuration.Configure(modelBuilder.Entity<Employee>());

            // Assert
            var entity = modelBuilder.Model.FindEntityType(typeof(Employee));
            entity.Should().NotBeNull();

            // Check key
            var key = entity.FindPrimaryKey();
            key.Should().NotBeNull();
            key.Properties.Should().Contain(p => p.Name == "Id");

            // Check required properties with max length
            var firstNameProperty = entity.FindProperty("FirstName");
            firstNameProperty.Should().NotBeNull();
            firstNameProperty.IsNullable.Should().BeFalse();
            firstNameProperty.GetMaxLength().Should().Be(50);

            var lastNameProperty = entity.FindProperty("LastName");
            lastNameProperty.Should().NotBeNull();
            lastNameProperty.IsNullable.Should().BeFalse();
            lastNameProperty.GetMaxLength().Should().Be(50);

            // Check self-referencing relationship
            var managerNavigation = entity.FindNavigation("Manager");
            managerNavigation.Should().NotBeNull();

            var subordinatesNavigation = entity.FindNavigation("Subordinates");
            subordinatesNavigation.Should().NotBeNull();

            // Check organization relationship
            var organizationNavigation = entity.FindNavigation("Organization");
            organizationNavigation.Should().NotBeNull();

            // Check address relationship
            var addressNavigation = entity.FindNavigation("Address");
            addressNavigation.Should().NotBeNull();
        }
    }
}