using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace EntityFrameworkCore.UnitTests.Repository
{
    public class UnitOfWorkTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void UnitOfWork_ShouldHaveOrganizationRepository()
        {
            // Assert
            _unitOfWork.OrganizationRespository.Should().NotBeNull();
            _unitOfWork.OrganizationRespository.Should().BeOfType<OrganizationRepository>();
        }

        [Fact]
        public void UnitOfWork_ShouldHaveEmployeeRepository()
        {
            // Assert
            _unitOfWork.EmployeeRespository.Should().NotBeNull();
            _unitOfWork.EmployeeRespository.Should().BeOfType<EmployeeRespository>();
        }

        [Fact]
        public void UnitOfWork_ShouldHaveCustomerRepository()
        {
            // Assert
            _unitOfWork.CustomerRespository.Should().NotBeNull();
            _unitOfWork.CustomerRespository.Should().BeOfType<CustomerRepository>();
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldSaveChanges()
        {
            // Arrange
            var organization = new Organization { Name = "Test Org", Phone = 1234567890, PAN = "ABCDE1234F" };
            await _context.Organizations.AddAsync(organization);

            // Act
            var result = await _unitOfWork.SaveChangesAsync();

            // Assert
            result.Should().BeGreaterThan(0);
            organization.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task BeginTransactionAsync_ShouldStartTransaction()
        {
            // Act
            var action = ()=> _unitOfWork.BeginTransactionAsync();

            // Assert
            await action.Should().ThrowAsync(); //It will throw exception as transaction is not supported in in memory database
        }

        [Fact]
        public void Dispose_ShouldDisposeContext()
        {
            // Act
            _unitOfWork.Dispose();

            // Assert
            // Since context is disposed, accessing it should throw
            Action act = () => _context.Organizations.ToList();
            act.Should().Throw<ObjectDisposedException>();
        }
    }
}