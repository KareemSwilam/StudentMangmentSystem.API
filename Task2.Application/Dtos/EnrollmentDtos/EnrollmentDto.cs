using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.CourseDtos;
using Task2.Application.Dtos.StudentDtos;
using Task2.Domain.Models;

namespace Task2.Application.Dtos.EnrollmentDtos
{
    public class EnrollmentDto
    {
        public CourseDto Course { get; set; }
        public StudentDto Student { get; set; }
        public double Grade { get; set; }
    }
}
