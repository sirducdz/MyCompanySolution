using MyCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project, int>
    {
    }
}
