using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Domain.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public DateOnly StartDate { get; set; }
        public List<Course> Courses { get; set; }
    }
}
