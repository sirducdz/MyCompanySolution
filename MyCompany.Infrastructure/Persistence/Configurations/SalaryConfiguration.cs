using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Infrastructure.Persistence.Configurations
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SalaryAmount)
                   .HasColumnType("decimal(18, 2)") // Specific SQL type
                   .IsRequired();

            // Configure One-to-One with Employee
            builder.HasOne(s => s.Employee)
                   .WithOne(e => e.Salary)
                   .HasForeignKey<Salary>(s => s.Id) // Foreign key is here
                   .IsRequired(); // Salary must belong to an Employee
        }
    }
}
