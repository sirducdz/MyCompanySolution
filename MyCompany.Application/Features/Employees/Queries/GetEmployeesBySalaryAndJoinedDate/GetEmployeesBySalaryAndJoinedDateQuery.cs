using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;

namespace MyCompany.Application.Features.Employees.Queries.GetEmployeesBySalaryAndJoinedDate
{
    public class GetEmployeesBySalaryAndJoinedDateQuery : IRequest<IEnumerable<EmployeeBasicInfoDto>>
    {
    }
}
