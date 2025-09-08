using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Application.Dtos.DepartmentDtos
{
    public class DepartmentDto
    {
        public string Name { get; set; }
        public double Budget { get; set; }
        public DateOnly StartDate { get; set; }
    }
}
