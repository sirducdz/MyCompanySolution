using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Application.Features.Salaries.Commands.UpdateSalary
{
    public class UpdateSalaryCommand : IRequest<Unit>
    {
        [Required]
        public int Id { get; set; } // Id của bản ghi Salary cần cập nhật

        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Salary amount must be non-negative.")]
        public decimal SalaryAmount { get; set; }

        // Không nên cho phép cập nhật EmployeeId ở đây, nó thể hiện mối quan hệ
    }
}
