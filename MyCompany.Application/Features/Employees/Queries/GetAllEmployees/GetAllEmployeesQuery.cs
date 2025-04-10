using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;

namespace MyCompany.Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
    {
        // Có thể thêm tham số phân trang/sắp xếp ở đây
    }
}
