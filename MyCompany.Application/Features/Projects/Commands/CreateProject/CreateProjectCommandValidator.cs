using FluentValidation;
using MyCompany.Domain.Interfaces;

namespace MyCompany.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandValidator(IProjectRepository projectRepository) // Constructor nhận Repository
        {
            _projectRepository = projectRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.")
                // Gọi phương thức từ Repository, projectIdToExclude là null hoặc 0 cho Create
                .MustAsync(async (name, cancellationToken) =>
                     await _projectRepository.IsNameUniqueAsync(name, null, cancellationToken)
                 ).WithMessage("A project with the same name already exists.");
        }
    }
}
