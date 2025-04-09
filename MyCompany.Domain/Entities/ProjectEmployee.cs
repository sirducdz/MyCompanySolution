using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Entities
{
    public class ProjectEmployee
    {
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public bool Enable { get; set; } = true;
    }
}
