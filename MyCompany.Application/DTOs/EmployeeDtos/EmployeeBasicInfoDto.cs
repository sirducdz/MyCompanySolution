namespace MyCompany.Application.DTOs.EmployeeDtos
{
    public class EmployeeBasicInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public int? DepartmentId { get; set; }
        public decimal SalaryAmount { get; set; }
    }
}
