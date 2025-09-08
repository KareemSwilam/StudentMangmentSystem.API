using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.DepartmentDtos;

namespace Task2.Application.Dtos.CourseDtos
{
    public class CourseDto
    {
        public string Title { get; set; }
        public int Credits { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
