using MediatR;

namespace MyCompany.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
