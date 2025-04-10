using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;

namespace MyCompany.Application.Features.Employees.Queries.GetEmployeesWithDepartment
{
    public class GetEmployeesWithDepartmentQuery : IRequest<IEnumerable<EmployeeWithDepartmentDto>>
    {
        // Không cần tham số.
    }
}
