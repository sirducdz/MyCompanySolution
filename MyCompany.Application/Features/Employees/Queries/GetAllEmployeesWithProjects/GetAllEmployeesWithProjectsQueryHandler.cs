using AutoMapper;
using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Employees.Queries.GetAllEmployeesWithProjects
{
    public class GetAllEmployeesWithProjectsQueryHandler : IRequestHandler<GetAllEmployeesWithProjectsQuery, IEnumerable<EmployeeWithProjectsDto>>
    {
        private readonly IUnitOfWork _unitOfWork; // Hoặc IUnitOfWork
        private readonly IMapper _mapper;

        public GetAllEmployeesWithProjectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<EmployeeWithProjectsDto>> Handle(GetAllEmployeesWithProjectsQuery request, CancellationToken cancellationToken)
        {
            var AllEmployeeWithProjects = await _unitOfWork.Employees.GetAllEmployeesAndTheirProjectsAsync();
            var EmployeeWithProjectsDtos = _mapper.Map<List<EmployeeWithProjectsDto>>(AllEmployeeWithProjects);

            return EmployeeWithProjectsDtos;
        }
    }
}
