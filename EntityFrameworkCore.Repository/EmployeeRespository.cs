using EntityFrameworkCore.Domain.Interfaces;

namespace EntityFrameworkCore.Repository
{
    internal class EmployeeRespository : IEmployeeRespository
    {
        private ApplicationDbContext applicationDbContext;

        public EmployeeRespository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext=applicationDbContext;
        }
    }
}