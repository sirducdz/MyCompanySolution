using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Application.Features.Salaries.Commands.CreateSalary
{
    public class CreateSalaryCommand : IRequest<int> // Trả về Id Salary mới
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Employee ID is required.")]
        public int EmployeeId { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Salary amount must be non-negative.")]
        public decimal SalaryAmount { get; set; }
    }
}
