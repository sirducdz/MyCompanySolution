using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Application.DTOs.DepartmentDtos;
using MyCompany.Application.Features.Departments.Commands.CreateDepartment;
using MyCompany.Application.Features.Departments.Commands.DeleteDepartment;
using MyCompany.Application.Features.Departments.Commands.UpdateDepartment;
using MyCompany.Application.Features.Departments.Queries.GetAllDepartments;
using MyCompany.Application.Features.Departments.Queries.GetDepartmentById;

namespace MyCompany.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator; // Inject IMediator

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Thông tin cho Swagger/OpenAPI
        public async Task<ActionResult<List<DepartmentDto>>> GetAllDepartments()
        {
            var query = new GetAllDepartmentsQuery();
            var departments = await _mediator.Send(query);
            return Ok(departments); // Trả về 200 OK cùng danh sách
        }

        [HttpGet("{id:int}", Name = "GetDepartmentById")] // Đặt tên route để dùng trong CreatedAtRoute
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var query = new GetDepartmentByIdQuery(id);
            var department = await _mediator.Send(query);

            if (department == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }

            return Ok(department); // Trả về 200 OK cùng DTO
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Nếu validation thất bại
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            // Validation được xử lý tự động bởi MediatR pipeline + FluentValidation
            var newDepartmentId = await _mediator.Send(command);

            // Lấy lại DTO của department vừa tạo để trả về (best practice)
            var createdDto = await _mediator.Send(new GetDepartmentByIdQuery(newDepartmentId));

            // Trả về 201 Created cùng với Location header và đối tượng vừa tạo
            return CreatedAtRoute("GetDepartmentById", new { id = newDepartmentId }, createdDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Handler sẽ ném NotFoundException nếu ko tìm thấy
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentCommand command)
        {
            // Kiểm tra xem ID từ route và ID trong body có khớp không
            if (id != command.Id)
            {
                return BadRequest("ID mismatch between route parameter and command body.");
            }

            // Gửi command, Handler sẽ xử lý hoặc ném Exception
            // NotFoundException sẽ được middleware bắt và trả về 404
            // ValidationException sẽ được middleware/pipeline bắt và trả về 400
            await _mediator.Send(command);

            return NoContent(); // Trả về 204 No Content khi cập nhật thành công
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Handler sẽ ném NotFoundException nếu ko tìm thấy
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var command = new DeleteDepartmentCommand(id);

            // Gửi command, Handler sẽ xử lý hoặc ném Exception
            await _mediator.Send(command);

            return NoContent(); // Trả về 204 No Content khi xóa thành công
        }
    }
}
