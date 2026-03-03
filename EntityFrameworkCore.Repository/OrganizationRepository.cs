using EntityFrameworkCore.Domain.Interfaces;

namespace EntityFrameworkCore.Repository
{
    internal class OrganizationRepository : IOrganizationRepository
    {
        private ApplicationDbContext applicationDbContext;

        public OrganizationRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext=applicationDbContext;
        }
    }
}