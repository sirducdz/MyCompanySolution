using MediatR;
using MyCompany.Application.DTOs.DepartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQuery : IRequest<List<DepartmentDto>>
    {
        // Không cần thuộc tính nào cho việc lấy tất cả đơn giản.
        // Có thể thêm các thuộc tính cho phân trang, sắp xếp, lọc ở đây nếu cần:
        // public int PageNumber { get; set; } = 1;
        // public int PageSize { get; set; } = 10;
        // public string SortOrder { get; set; }
    }
}
