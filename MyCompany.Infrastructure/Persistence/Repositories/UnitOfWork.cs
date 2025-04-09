using MyCompany.Domain.Interfaces;
using MyCompany.Infrastructure.DbContexts;

namespace MyCompany.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDepartmentRepository? _departments;
        private IEmployeeRepository? _employees;
        private IProjectRepository? _projects;
        private ISalaryRepository? _salaries;
        // ...

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Implement các property từ IUnitOfWork
        // Sử dụng ??= để khởi tạo lazy (chỉ tạo khi được gọi lần đầu)
        public IDepartmentRepository Departments => _departments ??= new DepartmentRepository(_context);
        public IEmployeeRepository Employees => _employees ??= new EmployeeRepository(_context);
        public IProjectRepository Projects => _projects ??= new ProjectRepository(_context);
        public ISalaryRepository Salaries => _salaries ??= new SalaryRepository(_context);


        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
            // Không cần dispose các repo vì chúng không quản lý resource riêng (chỉ dùng DbContext)
            GC.SuppressFinalize(this);
        }
    }
}
