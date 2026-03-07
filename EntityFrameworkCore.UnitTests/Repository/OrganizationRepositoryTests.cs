using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class OrganizationRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly OrganizationRepository _repository;

        public OrganizationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new OrganizationRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task AddAsync_ShouldAddOrganization()
        {
            // Arrange
            var organization = new Organization
            {
                Name = "Test Org",
                Phone = 123456789,
                PAN = "ABCDE1234F"
            };

            // Act
            var result = await _repository.AddAsync(organization);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be("Test Org");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrganization_WhenExists()
        {
            // Arrange
            var organization = new Organization
            {
                Name = "Existing Org",
                Phone = 987654321,
                PAN = "FGHIJ5678K"
            };
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(organization.Id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Existing Org");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrganizations()
        {
            // Arrange
            var org1 = new Organization { Name = "Org1", Phone = 111111111, PAN = "AAAAA1111A" };
            var org2 = new Organization { Name = "Org2", Phone = 222222222, PAN = "BBBBB2222B" };
            await _context.Organizations.AddRangeAsync(org1, org2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().HaveCountGreaterThanOrEqualTo(2);
            result.Should().Contain(o => o.Name == "Org1");
            result.Should().Contain(o => o.Name == "Org2");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateOrganization()
        {
            // Arrange
            var organization = new Organization
            {
                Name = "Original Org",
                Phone = 1234567890,
                PAN = "ABCDE1234F"
            };
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();

            organization.Name = "Updated Org";

            // Act
            var result = await _repository.UpdateAsync(organization);

            // Assert
            result.Name.Should().Be("Updated Org");
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenOrganizationExists()
        {
            // Arrange
            var organization = new Organization { Name = "ToDelete", Phone = 999999999, PAN = "ZZZZZ9999Z" };
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteAsync(organization.Id);

            // Assert
            result.Should().BeTrue();
            var deleted = await _context.Organizations.FindAsync(organization.Id);
            deleted.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenOrganizationNotExists()
        {
            // Act
            var result = await _repository.DeleteAsync(999);

            // Assert
            result.Should().BeFalse();
        }
    }
}