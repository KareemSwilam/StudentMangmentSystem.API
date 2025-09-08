using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Models.Dtos.DtosValidations
{
    public class StudentCreateDtoValidation:AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidation()
        {
            RuleFor(s => s.Name).NotEmpty().MinimumLength(5).WithMessage("Name Must be More than 5 character");
            RuleFor(s => s.Email).NotEmpty().EmailAddress().WithMessage("Email Must Contain @ Sign");
        }
    }
}
