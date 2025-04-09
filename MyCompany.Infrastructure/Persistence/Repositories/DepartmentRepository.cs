using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Interfaces;
using MyCompany.Infrastructure.DbContexts;

namespace MyCompany.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepository : Repository<Department, int>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Department>> GetAllWithEmployeesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(d => d.Employees).ToListAsync(cancellationToken);
        }

        public async Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(d => d.Name == name, cancellationToken);
        }

        // Các phương thức CRUD chung đã được implement ở lớp base Repository<Department, int>
    }
}
