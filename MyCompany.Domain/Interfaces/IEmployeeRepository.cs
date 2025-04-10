using MyCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
        Task<IEnumerable<Employee>> GetEmployeesJoinedAfterAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);
        Task<Employee?> GetEmployeeDetailsAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetEmployeesBySalaryAndJoinedDateAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetAllEmployeesAndTheirProjectsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetEmployeesWithDepartmentsAsync(CancellationToken cancellationToken = default);
    }
}
