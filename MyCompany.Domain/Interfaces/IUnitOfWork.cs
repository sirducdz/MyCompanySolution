using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Các property trả về interface repository cụ thể
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        IProjectRepository Projects { get; }
        ISalaryRepository Salaries { get; }
        // IProjectEmployeeRepository ProjectEmployees { get; } // Tùy chọn

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
