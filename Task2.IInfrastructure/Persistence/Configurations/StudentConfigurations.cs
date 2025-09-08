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
    internal class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Name).HasMaxLength(150);
            builder.HasMany(s => s.Courses)
                   .WithMany(c => c.Students)
                   .UsingEntity<Enrollment>(j =>
                   {
                       j.HasOne(e => e.Student)
                        .WithMany(s => s.Enrollments)
                        .HasForeignKey(e => e.StudentID);
                       j.HasOne(e => e.Course)
                        .WithMany(c => c.Enrollments)
                        .HasForeignKey(e => e.CourseID);
                   });
        }
    }
}
