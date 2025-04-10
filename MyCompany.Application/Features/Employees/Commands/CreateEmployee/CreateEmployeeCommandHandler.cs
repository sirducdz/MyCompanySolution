using AutoMapper;
using MediatR;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);
            await _unitOfWork.Employees.AddAsync(employee, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            Salary salary = new Salary
            {
                Id = employee.Id, // Lấy Id của nhân viên mới tạo
                SalaryAmount = request.InitialSalaryAmount ?? 0, // Nếu không có lương khởi tạo, mặc định là 0
            };
            await _unitOfWork.Salaries.AddAsync(salary, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return employee.Id; // Trả về Id mới
        }
    }
}
