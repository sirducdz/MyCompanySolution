namespace MyCompany.Application.DTOs.EmployeeDtos
{
    public class EmployeeWithDepartmentDto
    {
        // MappingProfile map từ Employee.Id (nên thêm vào DTO nếu cần)
        public int EmployeeId { get; set; }
        // MappingProfile map từ Employee.Name
        public string EmployeeName { get; set; } = string.Empty;
        // MappingProfile map từ Employee.Department.Name
        public string DepartmentName { get; set; } = string.Empty;
    }
}
