using AutoMapper;
using MyCompany.Application.DTOs.DepartmentDtos;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Application.DTOs.ProjectDtos;
using MyCompany.Application.DTOs.SalaryDtos;
using MyCompany.Application.Features.Departments.Commands.CreateDepartment;
using MyCompany.Application.Features.Departments.Commands.UpdateDepartment;
using MyCompany.Application.Features.Employees.Commands.CreateEmployee;
using MyCompany.Application.Features.Employees.Commands.UpdateEmployee;
using MyCompany.Application.Features.Projects.Commands.CreateProject;
using MyCompany.Application.Features.Projects.Commands.UpdateProject;
using MyCompany.Application.Features.Salaries.Commands.CreateSalary;
using MyCompany.Application.Features.Salaries.Commands.UpdateSalary;
using MyCompany.Domain.Entities;

namespace MyCompany.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // === Department Mappings ===
            CreateMap<Department, DepartmentDto>(); // Entity -> DTO
            CreateMap<CreateDepartmentCommand, Department>(); // Command -> Entity
            CreateMap<UpdateDepartmentCommand, Department>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()); // Không map Id từ command khi update

            // === Project Mappings ===
            CreateMap<Project, ProjectDto>(); // Entity -> DTO
            CreateMap<Project, ProjectBasicDto>(); // Entity -> DTO cơ bản (dùng cho list lồng nhau)
            CreateMap<CreateProjectCommand, Project>(); // Command -> Entity
            CreateMap<UpdateProjectCommand, Project>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()); // Không map Id

            // === Salary Mappings ===
            CreateMap<Salary, SalaryDto>(); // Entity -> DTO
            CreateMap<CreateSalaryCommand, Salary>(); // Command -> Entity
            //CreateMap<UpdateSalaryCommand, Salary>()
            //     .ForMember(dest => dest.Id, opt => opt.Ignore()) // Không map Id
            //     .ForMember(dest => dest.EmployeeId, opt => opt.Ignore()) // Không map EmployeeId
            //     .ForMember(dest => dest.Employee, opt => opt.Ignore()); // Không map Navigation Property


            // === Employee Mappings ===
            CreateMap<Employee, EmployeeDto>(); // Entity -> DTO cơ bản

            // Entity -> DTO Chi tiết (EmployeeDetailsDto)
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty)) // Lấy tên phòng ban
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary)) // Map đối tượng Salary (cần map Salary->SalaryDto)
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src =>
                    src.ProjectEmployees != null
                    ? src.ProjectEmployees.Select(pe => pe.Project) // Lấy danh sách Project từ bảng join (cần map Project->ProjectBasicDto)
                    : new List<Project>())); // Trả về list rỗng nếu ProjectEmployees là null


            // Command -> Entity (Create)
            CreateMap<CreateEmployeeCommand, Employee>()
                // AutoMapper sẽ tự map Name, JoinedDate, DepartmentId nếu tên giống nhau
                // Bỏ qua InitialSalaryAmount, việc tạo Salary sẽ xử lý trong Handler
                .ForMember(dest => dest.Salary, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore()) // Bỏ qua navigation properties
                .ForMember(dest => dest.ProjectEmployees, opt => opt.Ignore());

            // Command -> Entity (Update)
            CreateMap<UpdateEmployeeCommand, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Không map Id
                .ForMember(dest => dest.Salary, opt => opt.Ignore()) // Bỏ qua Salary (xử lý riêng nếu cần)
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectEmployees, opt => opt.Ignore());


            // === Mappings cho các DTO Query đặc biệt ===
            // Lưu ý: Sử dụng .Select() trực tiếp trong Handler thường hiệu quả hơn
            // cho các DTO này thay vì dùng AutoMapper sau khi Include().

            // Employee -> EmployeeWithDepartmentDto
            CreateMap<Employee, EmployeeWithDepartmentDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            // Employee -> EmployeeWithProjectsDto
            CreateMap<Employee, EmployeeWithProjectsDto>()
                   .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Projects, opt => opt.MapFrom(src =>
                   src.ProjectEmployees != null
                   ? src.ProjectEmployees.Select(pe => pe.Project) // Cần map Project -> ProjectBasicDto
                   : new List<Project>()));

            // Employee -> EmployeeBasicInfoDto (Dùng cho query lọc)
            CreateMap<Employee, EmployeeBasicInfoDto>()
               .ForMember(dest => dest.SalaryAmount, opt => opt.MapFrom(src => src.Salary != null ? src.Salary.SalaryAmount : 0m)); // Lấy SalaryAmount, xử lý null


            // === Assignment Commands ===
            // Các command như AssignProjectToEmployeeCommand thường không map trực tiếp
            // thành một Entity duy nhất. Logic xử lý chúng sẽ nằm trong Handler,
            // tạo hoặc cập nhật bản ghi ProjectEmployee dựa trên EmployeeId và ProjectId.
            // Do đó, thường không cần định nghĩa mapping cho chúng ở đây.

        }
    }
}
