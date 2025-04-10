using AutoMapper;
using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Employees.Queries.GetEmployeesWithDepartment
{
    public class GetEmployeesWithDepartmentQueryHandler : IRequestHandler<GetEmployeesWithDepartmentQuery, IEnumerable<EmployeeWithDepartmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeesWithDepartmentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeWithDepartmentDto>> Handle(GetEmployeesWithDepartmentQuery request, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.Employees.GetEmployeesWithDepartmentsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EmployeeWithDepartmentDto>>(employees);
        }
    }
}
