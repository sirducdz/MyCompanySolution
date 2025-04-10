using AutoMapper;
using MediatR;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Employees.Queries.GetEmployeesBySalaryAndJoinedDate
{
    public class GetEmployeesBySalaryAndJoinedDateQueryHandler : IRequestHandler<GetEmployeesBySalaryAndJoinedDateQuery, IEnumerable<EmployeeBasicInfoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeesBySalaryAndJoinedDateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeBasicInfoDto>> Handle(GetEmployeesBySalaryAndJoinedDateQuery request, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.Employees.GetEmployeesBySalaryAndJoinedDateAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EmployeeBasicInfoDto>>(employees);
        }
    }
}
