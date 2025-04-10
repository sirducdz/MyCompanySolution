using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;

namespace MyCompany.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDetailsDto?> // Nullable vì có thể không tìm thấy
    {
        public int Id { get; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
