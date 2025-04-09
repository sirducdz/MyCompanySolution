using MyCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department, int>
    {
        Task<IEnumerable<Department>> GetAllWithEmployeesAsync(CancellationToken cancellationToken = default);
        Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}