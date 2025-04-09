using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Interfaces;
using MyCompany.Infrastructure.DbContexts;

namespace MyCompany.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetEmployeesJoinedAfterAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(e => e.JoinedDate > date).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(e => e.DepartmentId == departmentId).ToListAsync(cancellationToken);
        }

        // Ví dụ override phương thức base nếu muốn thêm Include mặc định
        //public override async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
        //{
        //    return await _dbSet.Include(e => e.Department).Include(e => e.Salary).ToListAsync(cancellationToken);
        //}
    }
}
