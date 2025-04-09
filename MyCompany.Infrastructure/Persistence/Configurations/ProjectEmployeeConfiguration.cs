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
    public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployee>
    {
        public void Configure(EntityTypeBuilder<ProjectEmployee> builder)
        {
            // Define composite primary key
            builder.HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            // Configure relationship to Project
            builder.HasOne(pe => pe.Project)
                   .WithMany(p => p.ProjectEmployees)
                   .HasForeignKey(pe => pe.ProjectId);

            // Configure relationship to Employee
            builder.HasOne(pe => pe.Employee)
                   .WithMany(e => e.ProjectEmployees)
                   .HasForeignKey(pe => pe.EmployeeId);

            builder.Property(pe => pe.Enable)
                   .IsRequired(); // Bool is required by default, but explicit is fine
        }
    }
}
