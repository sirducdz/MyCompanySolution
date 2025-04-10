using MyCompany.Application.DTOs.ProjectDtos;

namespace MyCompany.Application.DTOs.EmployeeDtos
{
    public class EmployeeWithProjectsDto
    {
        // MappingProfile map từ Employee.Id (nên thêm vào DTO nếu cần)
        public int EmployeeId { get; set; }
        // MappingProfile map từ Employee.Name
        public string EmployeeName { get; set; } = string.Empty;
        // MappingProfile map từ Employee.ProjectEmployees.Select(pe => pe.Project)
        // Kết quả sẽ là danh sách ProjectBasicDto sau khi AutoMapper xử lý mapping Project -> ProjectBasicDto
        public List<ProjectBasicDto> Projects { get; set; } = new List<ProjectBasicDto>();
    }
}
