using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        [Required]
        public int Id { get; set; } // Id lấy từ route nhưng cần có trong command

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime JoinedDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Department.")]
        public int DepartmentId { get; set; }

        // Không bao gồm cập nhật Salary hoặc Projects ở đây cho đơn giản.
        // Cần tạo Command riêng nếu muốn, ví dụ: UpdateEmployeeSalaryCommand, AssignProjectToEmployeeCommand.
    }
}
