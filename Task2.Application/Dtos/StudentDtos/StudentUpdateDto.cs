using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Application.Dtos.StudentDtos
{
    public class StudentUpdateDto
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
    }
}
