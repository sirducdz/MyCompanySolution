using MyCompany.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Entities
{
    public class Salary : IEntity<int>
    {
        public int Id { get; set; }
        public decimal SalaryAmount { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
