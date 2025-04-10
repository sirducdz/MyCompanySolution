using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Application.Features.Assignments.Commands.UpdateEmployeeProjectLink
{
    public class UpdateEmployeeProjectLinkCommand : IRequest<Unit>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }

        [Required]
        public bool Enable { get; set; }
    }
}
