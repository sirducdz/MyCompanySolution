using AutoMapper;
using MediatR;
using MyCompany.Application.DTOs.DepartmentDtos;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = _unitOfWork.Departments.GetAll().ToList();
            var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);

            return departmentDtos;
        }
    }
}
