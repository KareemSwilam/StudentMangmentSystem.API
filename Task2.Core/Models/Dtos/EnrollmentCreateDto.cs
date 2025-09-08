using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Models.Dtos
{
    public class EnrollmentCreateDto
    {
        [Required]
        public int StudentID { get; set; }
        [Required]
        public int CourseID { get; set; }
    }
}
