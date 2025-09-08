using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Models.Dtos
{
    public class DepartmentCreateDto
    {
        [Required] 
        public string Name { get; set; }
        [Required]
        public double Budget { get; set; }
        //public DateOnly StartDate { get; set; }
    }
}
