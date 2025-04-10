using AutoMapper;
using MediatR;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);
            await _unitOfWork.Departments.AddAsync(department, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return department.Id; // Trả về Id mới
        }
    }
}
