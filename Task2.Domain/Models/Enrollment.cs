using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Domain.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public double Grade { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
