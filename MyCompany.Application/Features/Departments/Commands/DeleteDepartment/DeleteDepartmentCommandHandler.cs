using MediatR;
using MyCompany.Application.Exceptions;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(request.Id, cancellationToken);

            if (department == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Department), request.Id);
            }

            // Ví dụ: không cho xóa Department nếu còn Employee
            bool hasEmployees = _unitOfWork.Employees.Find(e => e.DepartmentId == request.Id).Any();
            // Kiểm tra xem có employee nào không
            if (hasEmployees)
            {
                throw new BadRequestException($"Cannot delete Department Id {request.Id}. It still has associated employees."); // Giả sử có BadRequestException
            }

            _unitOfWork.Departments.Remove(department);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
