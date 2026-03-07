using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository.Configurations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class OrganizationConfigurationTests
    {
        [Fact]
        public void OrganizationConfiguration_ShouldConfigureOrganizationEntity()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var configuration = new OrganizationConfiguration();

            // Act
            configuration.Configure(modelBuilder.Entity<Organization>());

            // Assert
            var entity = modelBuilder.Model.FindEntityType(typeof(Organization));
            entity.Should().NotBeNull();

            // Check key
            var key = entity.FindPrimaryKey();
            key.Should().NotBeNull();
            key.Properties.Should().Contain(p => p.Name == "Id");

            // Check required properties
            var nameProperty = entity.FindProperty("Name");
            nameProperty.Should().NotBeNull();
            nameProperty.IsNullable.Should().BeFalse();

            var panProperty = entity.FindProperty("PAN");
            panProperty.Should().NotBeNull();
            panProperty.IsNullable.Should().BeFalse();

            var createdDateProperty = entity.FindProperty("CreatedDate");
            createdDateProperty.Should().NotBeNull();
            createdDateProperty.IsNullable.Should().BeFalse();

            // Check address relationship
            var addressNavigation = entity.FindNavigation("Address");
            addressNavigation.Should().NotBeNull();
        }
    }
}