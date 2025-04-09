using MyCompany.Domain.Entities;
using MyCompany.Domain.Interfaces;
using MyCompany.Infrastructure.DbContexts;

namespace MyCompany.Infrastructure.Persistence.Repositories
{
    public class SalaryRepository : Repository<Salary, int>, ISalaryRepository
    {
        public SalaryRepository(ApplicationDbContext context) : base(context) { }
    }
}
