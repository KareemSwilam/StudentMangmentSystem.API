using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Domain.Models;

namespace Task2.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(150);
            builder.Property(d => d.StartDate).HasDefaultValue(DateTime.Now.AddYears(-5));
            builder.HasMany(d => d.Courses)
                   .WithOne(c => c.Department)
                   .HasForeignKey(c => c.DepartmentId);
            builder.Property(d => d.Budget).HasDefaultValue(150000);
            builder.Property(d => d.StartDate).HasDefaultValue(DateTime.UtcNow.AddYears(-5));
        }
    }
}
