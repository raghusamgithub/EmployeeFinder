using System;
using System.Collections.Generic;
using System.Text;
using AWE.Employee.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AWE.Employee.DAL.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee.DAL.Entities.Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee.DAL.Entities.Employee> entity)
        {
            entity.HasKey(e => e.EmployeeId);
            entity.ToTable("Employee");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId");
            entity.Property(e => e.EmployeeName).HasColumnName("EmployeeName");
        }
    }
}
