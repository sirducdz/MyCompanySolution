using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project, int>
    {
        Task<bool> IsNameUniqueAsync(string name, int? projectIdToExclude, CancellationToken cancellationToken);
    }
}
