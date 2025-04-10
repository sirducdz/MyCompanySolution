using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Application.Features.Assignments.Commands.AssignProjectToEmployee
{
    public class AssignProjectToEmployeeCommand : IRequest<Unit> // Hoặc trả về ID của ProjectEmployee nếu cần
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }

        // Có thể thêm trạng thái Enable nếu muốn set ngay lúc gán
        public bool Enable { get; set; } = true;
    }
}
