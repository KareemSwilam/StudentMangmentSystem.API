using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.StudentDtos;

namespace Task2.Application.Validations
{
    public class StudentCreateDtoValidation: AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidation()
        {
            RuleFor(s => s.Email)
                   .NotEmpty()
                   .EmailAddress()
                   .WithMessage("Email Must Contain @ Sign");
            RuleFor(s => s.Name).NotEmpty()
                   .MinimumLength(3)
                   .MaximumLength(25)
                   .WithMessage("Name Char between 3 to 25 char");
        }
    }
}
