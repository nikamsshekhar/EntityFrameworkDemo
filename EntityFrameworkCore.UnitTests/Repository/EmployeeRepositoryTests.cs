using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class EmployeeRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployeeRespository _repository;

        public EmployeeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new EmployeeRespository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task AddAsync_ShouldAddEmployee()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Role = Role.Employee
            };

            // Act
            var result = await _repository.AddAsync(employee);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.FirstName.Should().Be("John");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenExists()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "Jane",
                LastName = "Smith",
                Role = Role.Manager
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(employee.Id);

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
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var employee1 = new Employee { FirstName = "Alice", LastName = "Wonder", Role = Role.Employee };
            var employee2 = new Employee { FirstName = "Bob", LastName = "Builder", Role = Role.Manager };
            await _context.Employees.AddRangeAsync(employee1, employee2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().HaveCountGreaterThanOrEqualTo(2);
            result.Should().Contain(e => e.FirstName == "Alice");
            result.Should().Contain(e => e.FirstName == "Bob");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "Original",
                LastName = "Name",
                Role = Role.Employee
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            employee.FirstName = "Updated";

            // Act
            var result = await _repository.UpdateAsync(employee);

            // Assert
            result.FirstName.Should().Be("Updated");
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenEmployeeExists()
        {
            // Arrange
            var employee = new Employee { FirstName = "ToDelete", LastName = "User", Role = Role.Employee };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteAsync(employee.Id);

            // Assert
            result.Should().BeTrue();
            var deleted = await _context.Employees.FindAsync(employee.Id);
            deleted.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenEmployeeNotExists()
        {
            // Act
            var result = await _repository.DeleteAsync(999);

            // Assert
            result.Should().BeFalse();
        }
    }
}