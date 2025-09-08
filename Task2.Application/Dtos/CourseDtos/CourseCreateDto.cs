using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Domain.Models;

namespace Task2.Application.Dtos.CourseDtos
{
    public class CourseCreateDto
    {
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        
    }
}
