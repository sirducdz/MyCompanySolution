using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<Unit>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
    }
}
