using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Application.DTOs.EmployeeDtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public int DepartmentId { get; set; } // Có thể thêm DepartmentName nếu muốn join sẵn ở đây
    }
}
