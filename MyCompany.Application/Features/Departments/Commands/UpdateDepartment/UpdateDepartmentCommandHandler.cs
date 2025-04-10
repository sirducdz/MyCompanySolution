using AutoMapper;
using MediatR;
using MyCompany.Application.Exceptions;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Không dùng mapper để update trực tiếp entity

        public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Cần lấy entity CÓ TRACKING để update
            var department = await _unitOfWork.Departments.GetByIdAsync(request.Id, cancellationToken);

            if (department == null)
            {
                // Ném exception tùy chỉnh để middleware xử lý thành 404 Not Found
                throw new NotFoundException(nameof(Domain.Entities.Department), request.Id);
            }

            // Cập nhật thuộc tính trực tiếp trên entity đã được track
            department.Name = request.Name;
            // _unitOfWork.Departments.Update(department); // Không cần gọi Update tường minh nếu entity đã được track

            await _unitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value; // Trả về Unit cho request thành công không cần data
        }
    }
}
