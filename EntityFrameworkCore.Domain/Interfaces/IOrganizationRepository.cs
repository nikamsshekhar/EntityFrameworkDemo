using EntityFrameworkCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetByIdAsync(int id);
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<Organization> AddAsync(Organization organization);
        Task<Organization> UpdateAsync(Organization organization);
        Task<bool> DeleteAsync(int id);
    }
}
