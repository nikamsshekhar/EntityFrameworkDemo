using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class CustomerRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerRepository _repository;

        public CustomerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new CustomerRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task AddAsync_ShouldAddCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Type = CustomerType.Individual
            };

            // Act
            var result = await _repository.AddAsync(customer);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.FirstName.Should().Be("John");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenExists()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "Jane",
                LastName = "Smith",
                Type = CustomerType.Company
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(customer.Id);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Jane");
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
        public async Task GetAllAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customer1 = new Customer { FirstName = "Alice", LastName = "Wonder", Type = CustomerType.Individual };
            var customer2 = new Customer { FirstName = "Bob", LastName = "Builder", Type = CustomerType.Company };
            await _context.Customers.AddRangeAsync(customer1, customer2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().HaveCountGreaterThanOrEqualTo(2);
            result.Should().Contain(c => c.FirstName == "Alice");
            result.Should().Contain(c => c.FirstName == "Bob");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "Original",
                LastName = "Name",
                Type = CustomerType.Individual
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            customer.FirstName = "Updated";

            // Act
            var result = await _repository.UpdateAsync(customer);

            // Assert
            result.FirstName.Should().Be("Updated");
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { FirstName = "ToDelete", LastName = "User", Type = CustomerType.Individual };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteAsync(customer.Id);

            // Assert
            result.Should().BeTrue();
            var deleted = await _context.Customers.FindAsync(customer.Id);
            deleted.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenCustomerNotExists()
        {
            // Act
            var result = await _repository.DeleteAsync(999);

            // Assert
            result.Should().BeFalse();
        }
    }
}