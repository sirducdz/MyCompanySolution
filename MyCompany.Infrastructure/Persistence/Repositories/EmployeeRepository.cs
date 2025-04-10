using Microsoft.EntityFrameworkCore;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Application.DTOs.ProjectDtos;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Interfaces;
using MyCompany.Infrastructure.DbContexts;

namespace MyCompany.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetEmployeesBySalaryAndJoinedDateAsync(CancellationToken cancellationToken = default)
        {
            decimal salaryThreshold = 100;

            // Ngưỡng ngày tham gia (01/01/2024)
            DateTime joinedDateThreshold = new DateTime(2024, 1, 1);

            return await _dbSet
                .Where(employee => employee.Salary.SalaryAmount > salaryThreshold && employee.JoinedDate >= joinedDateThreshold)
                .Include(e => e.Salary) // Nạp thông tin Salary
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAndTheirProjectsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Include(e => e.ProjectEmployees)
                    .ThenInclude(ep => ep.Project)
                .ToListAsync(cancellationToken); // Thực thi truy vấn bất đồng bộ
        }

        public async Task<IEnumerable<Employee>> GetEmployeesJoinedAfterAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(e => e.JoinedDate > date).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(e => e.DepartmentId == departmentId).ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<Employee>> GetEmployeesWithDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            // Sử dụng Include để nạp (eager load) thông tin Department
            // EF Core sẽ tự động tạo INNER JOIN (nếu quan hệ là bắt buộc)
            //return await _context.Employees
            //    .Include(e => e.Department) // Nạp navigation property 'Department'
            //    .ToListAsync(cancellationToken);

            return await _context.Employees.Join(_context.Departments,
                               employee => employee.DepartmentId,
                                              department => department.Id,
                                                             (employee, department) => new Employee
                                                             {
                                                                 Id = employee.Id,
                                                                 Name = employee.Name,
                                                                 JoinedDate = employee.JoinedDate,
                                                                 DepartmentId = employee.DepartmentId,
                                                                 Department = department // Nạp thông tin Department
                                                             })
                .ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetEmployeeDetailsAsync(int id, CancellationToken cancellationToken)
        {
            // Ví dụ triển khai: Lấy Employee kèm Department, Salary, ProjectEmployees->Project
            // Sử dụng AsNoTracking vì đây là query chỉ đọc
            return await _context.Employees
                                .AsNoTracking()
                                .Include(e => e.Department)
                                .Include(e => e.Salary)
                                .Include(e => e.ProjectEmployees)
                                    .ThenInclude(pe => pe.Project) // Include Project từ bảng join
                                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

    }
}
