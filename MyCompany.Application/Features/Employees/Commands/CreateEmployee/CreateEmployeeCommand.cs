using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int> // Trả về Id của Employee mới
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime JoinedDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Department.")]
        public int DepartmentId { get; set; }

        // Tùy chọn: Thêm lương ngay khi tạo Employee
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Salary amount must be non-negative.")]
        public decimal? InitialSalaryAmount { get; set; }
    }
}
