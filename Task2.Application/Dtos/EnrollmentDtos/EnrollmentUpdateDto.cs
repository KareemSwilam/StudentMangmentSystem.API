using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Application.Dtos.EnrollmentDtos
{
    public class EnrollmentUpdateDto
    {
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public double Grade { get; set; }
    }
}
