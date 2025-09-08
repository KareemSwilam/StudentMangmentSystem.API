using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Models
{
    public class Student
    {
        
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            [EmailAddress]    
            public string Email { get; set; }
            //public List<Course> Courses { get; set; }
            public List<Enrollment> Enrollments { get; set; }
            
        
    }
}
