using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Application.Features.Employees.Queries.GetAllEmployeesWithProjects
{
    public class GetAllEmployeesWithProjectsQuery : IRequest<IEnumerable<EmployeeWithProjectsDto>>
    {
    }
}
