using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Domain.Interfaces;

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
            return _context.Organizations.ToList();
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