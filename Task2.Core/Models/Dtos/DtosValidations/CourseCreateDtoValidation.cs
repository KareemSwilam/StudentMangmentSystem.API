using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Models.Dtos.DtosValidations
{
    public class CourseCreateDtoValidation:AbstractValidator<CourseCreateDto>
    {
        public CourseCreateDtoValidation()
        {
            RuleFor(c => c.Credits).NotEmpty().WithMessage("Must enter the credits");
            RuleFor(C => C.DepartmentId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
        }
    }
}
