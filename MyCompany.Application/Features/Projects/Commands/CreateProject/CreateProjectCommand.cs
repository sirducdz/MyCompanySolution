using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<int> // Trả về Id project mới
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
    }
}
