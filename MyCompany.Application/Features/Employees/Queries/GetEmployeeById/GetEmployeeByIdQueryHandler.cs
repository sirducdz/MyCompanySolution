using AutoMapper;
using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailsDto?>
    {
        private readonly IUnitOfWork _unitOfWork; // Thay IEmployeeRepository bằng IUnitOfWork
        private readonly IMapper _mapper;

        // Constructor cập nhật để inject IUnitOfWork
        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EmployeeDetailsDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeDetailsAsync(request.Id, cancellationToken);

            if (employee == null)
            {
                return null; // Trả về null nếu không tìm thấy
            }

            var employeeDetailsDto = _mapper.Map<EmployeeDetailsDto>(employee);

            return employeeDetailsDto;
        }
    }
}
