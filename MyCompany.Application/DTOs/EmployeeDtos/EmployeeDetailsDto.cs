using MyCompany.Application.DTOs.ProjectDtos;
using MyCompany.Application.DTOs.SalaryDtos;

namespace MyCompany.Application.DTOs.EmployeeDtos
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }

        // Thông tin phòng ban
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        // Thông tin lương (có thể null nếu chưa có)
        public SalaryDto? Salary { get; set; } // Dùng SalaryDto đã định nghĩa

        // Danh sách dự án (có thể dùng ProjectBasicDto đã định nghĩa)
        public List<ProjectBasicDto> Projects { get; set; } = new List<ProjectBasicDto>();
    }
}
