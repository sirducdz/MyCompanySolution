using AutoMapper;
using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork; // Hoặc IUnitOfWork
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            // Lấy tất cả employee từ repository (nên dùng AsNoTracking bên trong repository)
            var employees = _unitOfWork.Employees.GetAll().ToList();

            // Map sang List<EmployeeDto>
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return employeeDtos;
        }
    }
}
