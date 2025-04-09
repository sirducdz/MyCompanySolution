using MyCompany.Domain.Interfaces;

namespace MyCompany.Domain.Entities
{
    public class Project : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }
}
