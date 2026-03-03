using EntityFrameworkCore.Domain.Interfaces;

namespace EntityFrameworkCore.Repository
{
    internal class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext applicationDbContext;

        public CustomerRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext=applicationDbContext;
        }
    }
}