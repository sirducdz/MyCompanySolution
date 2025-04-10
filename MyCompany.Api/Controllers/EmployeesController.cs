using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Application.DTOs.EmployeeDtos;
using MyCompany.Application.Features.Employees.Commands.CreateEmployee;
using MyCompany.Application.Features.Employees.Commands.UpdateEmployee;
using MyCompany.Application.Features.Employees.Queries.GetAllEmployees;
using MyCompany.Application.Features.Employees.Queries.GetAllEmployeesWithProjects;
using MyCompany.Application.Features.Employees.Queries.GetEmployeeById;
using MyCompany.Application.Features.Employees.Queries.GetEmployeesBySalaryAndJoinedDate;
using MyCompany.Application.Features.Employees.Queries.GetEmployeesWithDepartment;

namespace MyCompany.Api.Controllers
{
    [ApiController]
    [Route("api/employees")] // Định nghĩa route cơ sở cho controller này
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeesController> _logger; // Optional: Inject Logger

        public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // Thành công, trả về resource location
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Lỗi validation hoặc nghiệp vụ
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            // Validation được xử lý tự động bởi [ApiController] và FluentValidation middleware
            var newEmployeeId = await _mediator.Send(command);

            // Trả về 201 Created với header "Location" trỏ tới endpoint GetById
            // và body chứa ID của nhân viên mới tạo.
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployeeId }, new { id = newEmployeeId });
        }

        [HttpGet(Name = "Employees")]
        [ProducesResponseType(typeof(List<EmployeeDto>), StatusCodes.Status200OK)] // Thành công
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
        {
            var query = new GetAllEmployeesQuery(); // Có thể thêm tham số phân trang/lọc ở đây nếu query hỗ trợ
            var employees = await _mediator.Send(query);
            return Ok(employees); // Trả về 200 OK với danh sách employees
        }

        [HttpGet("with-projects", Name = "GetEmployeesWithProjects")]
        [ProducesResponseType(typeof(List<EmployeeWithProjectsDto>), StatusCodes.Status200OK)] // Thành công
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmployeeWithProjectsDto>>> GetEmployeesWithProjects()
        {
            var query = new GetAllEmployeesWithProjectsQuery(); // Có thể thêm tham số phân trang/lọc ở đây nếu query hỗ trợ
            var employees = await _mediator.Send(query);
            return Ok(employees); // Trả về 200 OK với danh sách employees
        }

        [HttpGet("with-department", Name = "GetEmployeesWithDepartment")]
        [ProducesResponseType(typeof(List<EmployeeWithDepartmentDto>), StatusCodes.Status200OK)] // Thành công
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmployeeWithDepartmentDto>>> GetEmployeesWithDepartment()
        {
            var query = new GetEmployeesWithDepartmentQuery(); // Có thể thêm tham số phân trang/lọc ở đây nếu query hỗ trợ
            var employees = await _mediator.Send(query);
            return Ok(employees); // Trả về 200 OK với danh sách employees
        }

        [HttpGet("with-salary-joinDate", Name = "GetEmployeesWithSalaryAndJoinDate")]
        [ProducesResponseType(typeof(List<EmployeeWithDepartmentDto>), StatusCodes.Status200OK)] // Thành công
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmployeeWithDepartmentDto>>> GetEmployeesWithSalaryAndJoinDate()
        {
            var query = new GetEmployeesBySalaryAndJoinedDateQuery(); // Có thể thêm tham số phân trang/lọc ở đây nếu query hỗ trợ
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }


        [HttpGet("{id:int}", Name = "GetEmployeeById")] // Đặt tên route để CreatedAtAction sử dụng
        [ProducesResponseType(typeof(EmployeeDetailsDto), StatusCodes.Status200OK)] // Tìm thấy
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Không tìm thấy
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDetailsDto>> GetEmployeeById(int id)
        {
            var query = new GetEmployeeByIdQuery(id);
            var employee = await _mediator.Send(query);

            // Handler GetEmployeeByIdQueryHandler trả về null nếu không tìm thấy
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID {EmployeeId} not found.", id);
                return NotFound(); // Trả về 404 Not Found
            }

            return Ok(employee); // Trả về 200 OK với chi tiết employee
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // Thành công, không có nội dung trả về
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Lỗi validation hoặc ID không khớp
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Không tìm thấy employee để cập nhật
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            // Đảm bảo ID trong route và trong body (nếu có) khớp nhau
            // Hoặc tốt hơn là chỉ dùng ID từ route
            if (id != command.Id)
            {
                _logger.LogWarning("Route ID {RouteId} does not match command ID {CommandId}.", id, command.Id);
                // Hoặc có thể bỏ qua ID trong command và gán từ route: command.Id = id;
                return BadRequest("Route ID must match Command ID.");
            }

            // Gán ID từ route vào command nếu command chưa có hoặc để đảm bảo tính nhất quán
            command.Id = id;

            // Gửi command, handler sẽ xử lý việc tìm, map và lưu
            // Handler UpdateEmployeeCommandHandler nên ném NotFoundException nếu không tìm thấy
            await _mediator.Send(command);

            // Trả về 204 No Content nếu thành công
            return NoContent();
        }
    }
}
