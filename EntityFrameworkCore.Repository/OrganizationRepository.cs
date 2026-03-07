using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Repository
{
    internal class OrganizationRepository : IOrganizationRepository
    {
        private ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Organization> AddAsync(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            if (organization !=null)
            {
                _context.Organizations.Remove(organization);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            // EF Core provides an asynchronous ToListAsync helper. The previous implementation
            // executed synchronously which meant the async/await keywords were unnecessary.
            return await _context.Organizations.ToListAsync();
        }

        public async Task<Organization> GetByIdAsync(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        public async Task<Organization> UpdateAsync(Organization organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
            return organization;
        }
    }
}