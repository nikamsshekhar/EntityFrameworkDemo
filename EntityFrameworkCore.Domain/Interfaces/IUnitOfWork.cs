using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrganizationRepository OrganizationRespository { get; }
        IEmployeeRespository EmployeeRespository { get; }
        ICustomerRepository CustomerRespository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
