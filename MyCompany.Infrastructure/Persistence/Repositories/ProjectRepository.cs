using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Interfaces;
using MyCompany.Infrastructure.DbContexts;

namespace MyCompany.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : Repository<Project, int>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context) { }
        public async Task<bool> IsNameUniqueAsync(string name, int? projectIdToExclude, CancellationToken cancellationToken)
        {
            var query = _context.Set<Project>()
                                 .Where(p => p.Name == name);

            if (projectIdToExclude.HasValue && projectIdToExclude.Value > 0)
            {
                query = query.Where(p => p.Id != projectIdToExclude.Value);
            }

            bool exists = await query.AnyAsync(cancellationToken);

            return !exists;
        }
    }
}
