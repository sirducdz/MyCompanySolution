using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Application.Features.Assignments.Commands.RemoveProjectFromEmployee
{
    public class RemoveProjectFromEmployeeCommand : IRequest<Unit>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }
    }
}
